namespace WarframeMod.Content.Items.Accessories;

public class MotusSignal : ModItem
{
    const int DOUBLE_JUMP_EXTRA_VELOCITY_PERCENT = 80;
    public const float DOUBLE_JUMP_VELOCITY_MULT = 1f + DOUBLE_JUMP_EXTRA_VELOCITY_PERCENT / 100f;
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
    bool[] couldDoubleJump;
    bool[] canDoubleJump;
    public override void PreUpdateMovement()
    {
        if (!enabled)
            return;
        bool touchingTiles = Player.TouchedTiles.Any();
        if (canDoubleJump != null)
            couldDoubleJump = canDoubleJump;
        else
            couldDoubleJump = new bool[Player.ExtraJumps.Length];
        canDoubleJump = Player.ExtraJumps.ToArray().Select(x => x.Available).ToArray();
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

        bool performingDoubleJump = Player.ExtraJumps.ToArray().Any(x => x.Active);
        if (performingDoubleJump)
        {
            Player.velocity.Y += doubleJumpExtraVelocity;
        }
    }
}