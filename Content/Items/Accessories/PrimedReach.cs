using WarframeMod.Common.GlobalItems;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class PrimedReach : ModItem
{
    public const int ABSOLUTE_RANGE_BONUS = 162;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 5;
        Item.value = Item.buyPrice(gold: 3);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Reach>());
        recipe.AddIngredient(ItemID.SoulofFright, 6);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<TrueMeleeRangePlayer>().absoluteExtraRange += ABSOLUTE_RANGE_BONUS;
    }
}