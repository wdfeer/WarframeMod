using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class VileAcceleration : ModItem
{
    public const int USE_SPEED_INCREASE_PERCENT = 20;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 2);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetAttackSpeed(DamageClass.Generic) += USE_SPEED_INCREASE_PERCENT / 100f;
        player.GetDamage(DamageClass.Generic) -= 0.15f;
    }
}
