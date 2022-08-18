﻿using WarframeMod.Global;
using WarframeMod.Items.Accessories;

namespace WarframeMod.Players;

internal class CritsPlayer : ModPlayer
{
    public float critMultiplierPlayer = 1f;
    public float relativeCritChance = 0f;
    public override void ResetEffects()
    {
        critMultiplierPlayer = 1f;
        relativeCritChance = 0f;
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
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        var critLevel = GetCritLevel(Player.GetWeaponCrit(item));
        if (crit && critLevel > 0)
        {
            float mult = critMultiplierPlayer;
            damage = (int)(damage * mult * critLevel);
            OverCritVisuals(target, knockback, critLevel);

            Player.GetModPlayer<HunterMunitionsPlayer>().TryBleed(target, damage * 2);
        }
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        var critLevel = GetCritLevel(proj.CritChance);
        if (crit && critLevel > 0)
        {
            float mult = critMultiplierPlayer * proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier;
            damage = (int)(damage * mult * critLevel);
            OverCritVisuals(target, knockback, critLevel);

            Player.GetModPlayer<HunterMunitionsPlayer>().TryBleed(target, damage * 2);
        }
    }
    void OverCritVisuals(NPC target, float knockback, int critlvl)
    {
        if (critlvl < 2) return;
        Color critColor = CombatText.DamagedHostile;
        switch (critlvl)
        {
            case 2:
                critColor = Color.Orange;
                break;
            default:
                critColor = Color.Red;
                break;
        }
        target.GetGlobalNPC<OvercritNPCVisuals>().thisCritColor = critColor;
    }
}
