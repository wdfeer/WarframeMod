using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class Vitality : ModItem
{
    public const int EXTRA_MAX_LIFE = 50;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{EXTRA_MAX_LIFE} max life");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 1;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(silver: 20);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statLifeMax2 += EXTRA_MAX_LIFE;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GreenSlimeBanner, 1);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}