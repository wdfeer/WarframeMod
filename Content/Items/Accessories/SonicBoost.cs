using Terraria.Localization;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class SonicBoost : ModItem
{
    public const int HORIZONTAL_WING_SPEED_PERCENT = 20;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(HORIZONTAL_WING_SPEED_PERCENT);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 6;
        Item.value = Item.sellPrice(gold: 24);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Gi);
        recipe.AddIngredient(ItemID.FastClock);
        recipe.AddIngredient(ItemID.SoulofFlight, 15);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<WingSpeedPlayer>().horizontalWingSpeedMult += HORIZONTAL_WING_SPEED_PERCENT / 100f;
        player.GetModPlayer<SonicBoostPlayer>().active = true;
    }
}

class SonicBoostPlayer : ModPlayer
{
    public bool active;
    public override void ResetEffects() => active = false;

    public override void PreUpdateMovement()
    {
        if (active && Player.dashDelay != 0)
        {
            Player.velocity.X = MathF.Min(Player.velocity.X * 1.018f, 15f);
        }
    }
}