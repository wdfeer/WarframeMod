﻿namespace WarframeMod.Content.Items.Accessories;

public class NaturalTalent : ModItem
{
    public const int MAGIC_USE_SPEED_INCREASE_PERCENT = 12;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 4);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetAttackSpeed(DamageClass.Magic) += MAGIC_USE_SPEED_INCREASE_PERCENT / 100f;
    }
}
