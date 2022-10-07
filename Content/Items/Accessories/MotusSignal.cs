namespace WarframeMod.Content.Items.Accessories;

public class MotusSignal : ModItem
{
    const int DOUBLE_JUMP_EXTRA_VELOCITY_PERCENT = 80;
    public const float DOUBLE_JUMP_VELOCITY_MULT = 1f + DOUBLE_JUMP_EXTRA_VELOCITY_PERCENT / 100f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{DOUBLE_JUMP_EXTRA_VELOCITY_PERCENT}% Double Jump velocity");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 50);
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
        player.GetModPlayer<MotusSignalPlayer>().enabled = true;
    }
}
class MotusSignalPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    float doubleJumpExtraVelocity = 0;
    bool[] couldDoubleJump = new bool[5] { false, false, false, false, false };
    bool[] canDoubleJump = new bool[5] { false, false, false, false, false };
    public override void PreUpdateMovement()
    {
        if (!enabled)
            return;
        bool touchingTiles = Player.TouchedTiles.Any();
        couldDoubleJump = canDoubleJump;
        canDoubleJump = new bool[]
        {
            Player.canJumpAgain_Blizzard,
            Player.canJumpAgain_Cloud,
            Player.canJumpAgain_Fart,
            Player.canJumpAgain_Sail,
            Player.canJumpAgain_Sandstorm
        };
        if (touchingTiles)
            return;

        bool started = false;
        for (int i = 0; i < canDoubleJump.Length; i++)
        {
            started = !canDoubleJump[i] && couldDoubleJump[i];
            if (started)
            {
                float acceleration = Player.velocity.Y - Player.oldVelocity.Y;
                doubleJumpExtraVelocity = acceleration * (MotusSignal.DOUBLE_JUMP_VELOCITY_MULT - 1);
                break;
            }
        }

        bool performingDoubleJump = Player.isPerformingJump_Blizzard
                                   || Player.isPerformingJump_Cloud
                                   || Player.isPerformingJump_Fart
                                   || Player.isPerformingJump_Sail
                                   || Player.isPerformingJump_Sandstorm;
        if (performingDoubleJump)
        {
            Player.velocity.Y += doubleJumpExtraVelocity;
        }
    }
}