using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class HollowPoint : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+25% Critical Damage, but -15% damage");
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
        player.GetModPlayer<CritPlayer>().critMultiplierPlayer += 0.25f;
        player.GetDamage(DamageClass.Generic) -= 0.15f;
    }
}
