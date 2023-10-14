using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;
public class HyperionThrusters : ModItem
{
    public const float VERTICAL_WING_SPEED_BONUS = 0.33f;

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 4;
        Item.value = Item.buyPrice(gold: 1);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.OrichalcumBar, 5);
        recipe.AddIngredient(ItemID.Feather, 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MythrilBar, 5);
        recipe.AddIngredient(ItemID.Feather, 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<WingSpeedPlayer>().verticalWingSpeedMult += VERTICAL_WING_SPEED_BONUS;
    }
}