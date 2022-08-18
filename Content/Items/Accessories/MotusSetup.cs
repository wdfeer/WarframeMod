using WarframeMod.Common.Players;

namespace WarframeMod.Items.Accessories;

public class MotusSetup : ModItem
{
    public const int CritChanceBonus = 100;
    public const int BonusTime = 4;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{CritChanceBonus}% Relative Critical Chance after landing from a Double Jump for {BonusTime} seconds");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 3;
        Item.value = Terraria.Item.sellPrice(gold: 2);
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
            Buff(MotusSetup.BonusTime * 60);
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
            Player.GetModPlayer<CritsPlayer>().relativeCritChance += MotusSetup.CritChanceBonus / 100f;
        }
    }
    int bufftime = -1;
    void Buff(int frames)
    {
        bufftime = frames;
    }
}