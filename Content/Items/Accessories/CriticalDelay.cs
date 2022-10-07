﻿using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class CriticalDelay : ModItem
{
    public const int PERCENT_CRIT_RELATIVE = 80;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{PERCENT_CRIT_RELATIVE}% Crit Chance relative to the weapon's base crit chance, but -15% Use Speed");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 3);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<CritPlayer>().relativeCritChance += PERCENT_CRIT_RELATIVE / 100f;
        player.GetAttackSpeed(DamageClass.Generic) -= 0.15f;
    }
}
