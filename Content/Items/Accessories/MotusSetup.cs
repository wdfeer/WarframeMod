using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class MotusSetup : ModItem
{
    public const int CRIT_RELATIVE_PERCENT = CriticalDelay.PERCENT_CRIT_RELATIVE;
    public const int DURATION_SECONDS = 6;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 1);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HarpyBanner);
        recipe.AddTile(TileID.Solidifier);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<MotusSetupPlayer>().enabled = true;
    }
}
class MotusSetupPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
    {
        enabled = false;
    }
    public bool doubleJumpingOrWinging;
    public override void PostUpdate()
    {
        if (!enabled)
            return;
        bool touchingTiles = Player.TouchedTiles.Any();
        if (touchingTiles && doubleJumpingOrWinging)
        {
            Player.AddBuff(ModContent.BuffType<MotusSetupBuff>(), MotusSetup.DURATION_SECONDS * 60);
            doubleJumpingOrWinging = false;
        }
        else if (!doubleJumpingOrWinging)
        {
            doubleJumpingOrWinging = Player.isPerformingJump_Blizzard
                || Player.isPerformingJump_Cloud
                || Player.isPerformingJump_Fart
                || Player.isPerformingJump_Sail
                || Player.isPerformingJump_Sandstorm;
        }
    }
}
class MotusSetupGlobalItem : GlobalItem
{
    public override bool WingUpdate(int wings, Player player, bool inUse)
    {
        if (inUse)
            player.GetModPlayer<MotusSetupPlayer>().doubleJumpingOrWinging = true;
        return false;
    }
}