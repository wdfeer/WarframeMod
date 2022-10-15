using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class VileAcceleration : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+20% Use Speed, but -10% damage");
    }
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
        player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
        player.GetDamage(DamageClass.Generic) -= 0.1f;
    }
}
