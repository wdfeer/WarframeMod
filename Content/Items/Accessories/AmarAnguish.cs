namespace WarframeMod.Content.Items.Accessories;

public class AmarAnguish : AmarAccessory
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 33);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        player.moveSpeed += 0.08f;
        player.jumpSpeedBoost += 1f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrimtaneBar, 2);
        recipe.AddIngredient(ItemID.Aglet);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DemoniteBar, 2);
        recipe.AddIngredient(ItemID.Aglet);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
