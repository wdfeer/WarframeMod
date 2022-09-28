using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class ArmoredAgility : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+6% damage resistance\n+10% movement speed");
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
        player.GetModPlayer<DamageResistancePlayer>().AddDR(0.06f);
        player.moveSpeed += 0.1f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Shackle);
        recipe.AddIngredient(ItemID.Aglet);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}
