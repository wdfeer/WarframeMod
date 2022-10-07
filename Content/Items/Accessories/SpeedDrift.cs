using System.Threading;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class SpeedDrift : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+20% movement speed\n+8% weapon use speed");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.moveSpeed += 0.2f;
        player.GetAttackSpeed(DamageClass.Generic) += 0.08f;
    }
    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ItemID.AnkletoftheWind)
                         .AddIngredient(ItemID.Feather, 5)
                         .AddTile(TileID.Anvils)
                         .Register();
}
