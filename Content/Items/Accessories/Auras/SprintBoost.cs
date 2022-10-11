using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories.Auras;
public class SprintBoost : ModItem
{
    public const float MOVE_SPEED = 0.15f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{(int)(MOVE_SPEED * 100)}% movement speed to players on your team");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 1, silver: 15);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AnkletoftheWind);
        recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AuraPlayer>().myAuras.sprintBoost = true;
    }
}
class SprintBoostPlayer : ModPlayer
{
    public int Count => Player.GetModPlayer<AuraPlayer>().CountAurasInMyTeam(x => x.sprintBoost);
    public override void PostUpdateEquips()
    {
        Player.moveSpeed += SprintBoost.MOVE_SPEED * Count;
    }
}