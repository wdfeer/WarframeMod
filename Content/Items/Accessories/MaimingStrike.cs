﻿namespace WarframeMod.Content.Items.Accessories;

public class MaimingStrike : ModItem
{
    public const int WHIP_CRIT = 20;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetCritChance(DamageClass.SummonMeleeSpeed) += WHIP_CRIT;
    }
}
