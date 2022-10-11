using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class ArmoredAgility : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+15% movement speed\n+7.5% damage resistance");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 33);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<DamageResistancePlayer>().AddDR(0.075f);
        player.moveSpeed += 0.15f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AnkletoftheWind);
        recipe.AddIngredient(ItemID.Shackle);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}
