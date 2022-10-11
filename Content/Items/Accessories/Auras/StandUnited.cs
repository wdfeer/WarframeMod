using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories.Auras;
public class StandUnited : ModItem
{
    public const float EXTRA_DEFENSE = 0.15f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{(int)(EXTRA_DEFENSE * 100)}% defense to players on your team");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 1);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HellstoneBar, 3);
        recipe.AddIngredient(ItemID.MeteoriteBar, 3);
        recipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AuraPlayer>().myAuras.standUnited = true;
    }
}
class StandUnitedPlayer : ModPlayer
{
    public int Count => Player.GetModPlayer<AuraPlayer>().CountAurasInMyTeam(x => x.standUnited);
    public override void PostUpdateMiscEffects()
    {
        Player.statDefense += (int)(Player.statDefense * StandUnited.EXTRA_DEFENSE * Count);
    }
}