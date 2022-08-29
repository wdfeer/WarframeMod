using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class MotusSetup : ModItem
{
    public const int CRIT_CHANCE = 100;
    public const int DURATION = 4;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{CRIT_CHANCE}% Relative Critical Chance after landing from a Double Jump for {DURATION} seconds");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 2);
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
    bool doubleJumping;
    public override void PostUpdate()
    {
        if (!enabled)
            return;
        bool touchingTiles = Player.TouchedTiles.Any();
        if (touchingTiles && doubleJumping)
        {
            Buff(MotusSetup.DURATION * 60);
            doubleJumping = false;
        }
        else if (!doubleJumping)
        {
            doubleJumping = Player.isPerformingJump_Blizzard
                || Player.isPerformingJump_Cloud
                || Player.isPerformingJump_Fart
                || Player.isPerformingJump_Sail
                || Player.isPerformingJump_Sandstorm;
        }
    }
    public override void PreUpdateBuffs()
    {
        if (bufftime > 0)
        {
            bufftime--;
            Player.GetModPlayer<CritPlayer>().relativeCritChance += MotusSetup.CRIT_CHANCE / 100f;
        }
    }
    int bufftime = -1;
    void Buff(int frames)
    {
        bufftime = frames;
    }
}