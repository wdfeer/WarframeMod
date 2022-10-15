﻿using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Accessories;

public class HunterCommand : HunterAccessory
{
    public const int DAMAGE_ON_BLEEDING_PERCENT = 15;
    public override string DefaultTooltip => $"+{DAMAGE_ON_BLEEDING_PERCENT}% summon damage on bleeding enemies as a separate multiplier";
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 4);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        player.GetModPlayer<HunterCommandPlayer>().enabled = true;
    }
}
class HunterCommandPlayer : ModPlayer
{
    public bool enabled = false;
    public override void ResetEffects()
        => enabled = false;
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (enabled && (proj.DamageType == DamageClass.Summon || proj.DamageType == DamageClass.SummonMeleeSpeed) && target.GetGlobalNPC<StackableDebuffNPC>().bleeds.Any())
            damage += (int)(damage * HunterCommand.DAMAGE_ON_BLEEDING_PERCENT / 100f);
    }
}
