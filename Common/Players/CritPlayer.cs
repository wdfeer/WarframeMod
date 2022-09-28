using Steamworks;
using WarframeMod.Common.Configs;
using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Common.Players;

internal class CritPlayer : ModPlayer
{
    public float critMultiplierPlayer = 1f;
    public float relativeCritChance = 0f;
    public int summonCritChance = 0;
    public float summonCritMult = 1f;
    public override void ResetEffects()
    {
        critMultiplierPlayer = 1f;
        relativeCritChance = 0f;
        summonCritChance = 0;
        summonCritMult = 1f;
    }
    public override void PostUpdateEquips()
    {
        if (Player.HeldItem.damage > 0)
            Player.GetCritChance(DamageClass.Generic) += (Player.HeldItem.crit + 4) * relativeCritChance;
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
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        if (!crit)
            return;
        CritPlayerHooks[] hookers = GetHookers();
        var critLevel = GetCritLevel(Player.GetWeaponCrit(item));
        if (critLevel < 1)
            critLevel = 1;
        float mult = critMultiplierPlayer;
        int oldDamage = damage;
        foreach (var h in hookers)
        {
            h.ModifyHitNPCPreCrit(item, target, ref damage, ref knockback, ref crit, ref mult, ref critLevel);
        } 
        damage = (int)(damage * mult * critLevel);
        OverCritVisuals(target, knockback, critLevel);
        foreach (var h in hookers)
        {
            h.OnHitNPCPostCrit(item, target, oldDamage, knockback, crit, mult, critLevel, damage * (crit ? 2 : 1));
        } 
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        int critLevel;
        if (proj.DamageType == DamageClass.Summon || proj.DamageType == DamageClass.SummonMeleeSpeed)
        {
            critLevel = GetCritLevel(summonCritChance);
            crit = critLevel > 0;
        }
        else
            critLevel = GetCritLevel(proj.CritChance);
        if (!crit)
            return;
        CritPlayerHooks[] hookers = GetHookers();
        if (critLevel < 1)
            critLevel = 1;
        float mult = GetProjectileCritMult(proj);
        int oldDamage = damage;
        foreach (var h in hookers)
        {
            h.ModifyHitNPCWithProjPreCrit(proj, target, ref damage, ref knockback, ref crit, ref mult, ref critLevel, ref hitDirection);
        }
        damage = (int)(damage * mult * critLevel);
        foreach (var h in hookers)
        {
            h.OnHitNPCWithProjPostCrit(proj, target, oldDamage, knockback, crit, mult, critLevel, damage * (crit ? 2 : 1));
        }
        OverCritVisuals(target, knockback, critLevel);

        proj.GetGlobalProjectile<BuffGlobalProjectile>().HitNPCAfterCritModifiersApplied(target, damage * 2);
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
    public static int GetPostCritDamage(int preCrit, int critLvl, float critMult)
    {
        if (critLvl <= 0)
            return preCrit;
        return (int)(preCrit * 2 * critMult * critLvl);
    }
}
public abstract class CritPlayerHooks : ModPlayer
{
    public virtual void OnHitNPCPostCrit(Item item, NPC target, int damage, float knockback, bool crit, float critMult, int critLvl, int damagePostCrit) { }
    public virtual void OnHitNPCWithProjPostCrit(Projectile proj, NPC target, int damage, float knockback, bool crit, float critMult, int critLvl, int damagePostCrit) { }
    public virtual void ModifyHitNPCPreCrit(Item item, NPC target, ref int damage, ref float knockback, ref bool crit, ref float critMult, ref int critLvl) { }
    public virtual void ModifyHitNPCWithProjPreCrit(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref float critMult, ref int critLvl, ref int hitDirection) { }
}