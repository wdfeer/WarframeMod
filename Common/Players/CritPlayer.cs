﻿using WarframeMod.Common.Configs;
using WarframeMod.Common.GlobalItems;
using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.Players;

internal class CritPlayer : ModPlayer
{
    public float critMultiplierPlayer = 1f;
    public int[] GetItemTypesThatDoNotUseRelativeCrit() => new int[] { ModContent.ItemType<Nataruk>() };
    public float relativeCritChance = 0f;
    public float BaseCritChanceMult => relativeCritChance + 1f;
    public int summonCritChance = 0;
    public float summonCritMult = 1f;
    public override void ResetEffects()
    {
        critMultiplierPlayer = 1f;
        relativeCritChance = 0f;
        summonCritChance = 0;
        summonCritMult = 1f;
    }
    public override void ModifyWeaponCrit(Item item, ref float crit)
    {
        if (item.damage > 0 && !GetItemTypesThatDoNotUseRelativeCrit().Contains(item.type))
            crit += (item.crit + 4) * relativeCritChance;
        if (IsItemSummon(item))
            crit += summonCritChance;
    }
    public int GetCritLevel(int critChance)
    {
        int lvl = 0;
        while (critChance > 0)
        {
            if (Main.rand.Next(0, 101) < critChance)
                lvl++;
            critChance -= 100;
        }
        return lvl;
    }
    CritPlayerHooks[] GetHookers()
    {
        List<CritPlayerHooks> hookers = new();
        for (int i = 0; i < Player.ModPlayers.Length; i++)
        {
            var element = Player.ModPlayers[i];
            if (element is CritPlayerHooks)
                hookers.Add(element as CritPlayerHooks);
        }
        return hookers.ToArray();
    }
    public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
    {
        CritPlayerHooks[] hookers = GetHookers();
        var critLevel = GetCritLevel(Player.GetWeaponCrit(item));
        bool crit = critLevel > 0;


        float mult = critMultiplierPlayer * item.GetGlobalItem<CritGlobalItem>().critMultiplier;
        int oldDamage = modifiers.GetDamage(item.damage, crit);
        foreach (var h in hookers)
        {
            h.ModifyHitNPCPreCrit(item, target, ref modifiers, ref crit, ref mult, ref critLevel);
        }
        if (crit)
        {
            modifiers.SetCrit();
            modifiers.SourceDamage *= mult * critLevel;
        }
        else
            modifiers.DisableCrit();
        OverCritVisuals(target, modifiers.GetKnockback(item.knockBack), critLevel);
        int postCritDmg = modifiers.GetDamage(item.damage, crit);
        foreach (var h in hookers)
        {
            h.OnHitNPCPostCrit(item, target, oldDamage, modifiers.GetKnockback(item.knockBack), crit, mult, critLevel, postCritDmg);
        }
        if (Main.rand.NextFloat() < item.GetGlobalItem<BleedingGlobalItem>().bleedingChance)
            BleedingBuff.Create(postCritDmg, target);
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
    {
        bool crit;
        int critLevel = GetCritLevel(proj.CritChance);
        crit = critLevel > 0;

        CritPlayerHooks[] hookers = GetHookers();

        float mult = GetProjectileCritMult(proj);
        int oldDamage = modifiers.GetDamageWithoutDefense(proj.damage, crit);
        foreach (var h in hookers)
        {
            h.ModifyHitNPCWithProjPreCrit(proj, target, ref modifiers, ref crit, ref mult, ref critLevel);
        }
        if (crit)
        {
            modifiers.SetCrit();
            modifiers.SourceDamage *= critLevel * mult;
        }
        else modifiers.DisableCrit();

        int newDamage = modifiers.GetDamageWithoutDefense(proj.damage, crit);
        foreach (var h in hookers)
        {
            h.OnHitNPCWithProjPostCrit(proj, target, oldDamage, modifiers.GetKnockback(proj.knockBack), crit, mult, critLevel, newDamage);
        }
        OverCritVisuals(target, modifiers.GetKnockback(proj.knockBack), critLevel);

        proj.GetGlobalProjectile<BuffGlobalProjectile>().HitNPCAfterCritModifiersApplied(target, newDamage);
    }
    public static Color GetCritColor(int critLvl)
    {
        switch (critLvl)
        {
            case < 1:
                return ModContent.GetInstance<WarframeClientConfig>().noCritHitColor;
            case 1:
                return ModContent.GetInstance<WarframeClientConfig>().tier1CritColor;
            case 2:
                return ModContent.GetInstance<WarframeClientConfig>().tier2CritColor;
            default:
                return ModContent.GetInstance<WarframeClientConfig>().maxCritColor;
        }
    }
    void OverCritVisuals(NPC target, float knockback, int critlvl)
    {
        if (Main.myPlayer != Player.whoAmI)
            return;
        var overcritNPC = target.GetGlobalNPC<OvercritNPCVisuals>();
        overcritNPC.nextCritLevel = critlvl;
    }
    public override void PostUpdate()
    {
        for (int i = 0; i < Main.maxCombatText; i++)
        {
            CombatText ct = Main.combatText[i];
            if (ct == null || !ct.active)
                continue;
            if (ct.color == new Color(102, 64, 32, 102))
                ct.color = GetCritColor(0);
            else if (ct.color == new Color(102, 40, 12, 102))
                ct.color = GetCritColor(1);
        }
    }
    public float GetProjectileCritMult(Projectile proj)
    {
        float mult = critMultiplierPlayer * proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier;
        if (proj.DamageType == DamageClass.Summon || proj.DamageType == DamageClass.SummonMeleeSpeed)
            mult *= summonCritMult;
        return mult;
    }
    public static float GetTotalCritMult(int critLvl, float critMult)
    {
        if (critLvl <= 0)
            return 1;
        return 2 * critMult * critLvl;
    }
    public static bool IsItemSummon(Item item) => item.DamageType == DamageClass.Summon || item.DamageType == DamageClass.SummonMeleeSpeed;
}
public abstract class CritPlayerHooks : ModPlayer
{
    public virtual void OnHitNPCPostCrit(Item item, NPC target, int damage, float knockBack, bool crit, float critMult, int critLvl, int damagePostCrit) { }
    public virtual void OnHitNPCWithProjPostCrit(Projectile proj, NPC target, int damage, float knockBack, bool crit, float critMult, int critLvl, int damagePostCrit) { }
    public virtual void ModifyHitNPCPreCrit(Item item, NPC target, ref NPC.HitModifiers modifiers, ref bool crit, ref float critMult, ref int critLvl) { }
    public virtual void ModifyHitNPCWithProjPreCrit(Projectile proj, NPC target, ref NPC.HitModifiers modifiers, ref bool crit, ref float critMult, ref int critLvl) { }
}