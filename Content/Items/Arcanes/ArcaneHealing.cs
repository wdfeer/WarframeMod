using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneHealing : Arcane
{
    private const int RESTORE_INCREASE_PERCENT = 20;
    public const float RESTORE_MULT = (1f + RESTORE_INCREASE_PERCENT / 100f);
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RESTORE_INCREASE_PERCENT);
    public override void UpdateArcane(Player player)
    {
        player.buffImmune[BuffID.Confused] = true;
        player.GetModPlayer<ArcaneHealingPlayer>().enabled = true;
    }
}
class ArcaneHealingPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    
    public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
    {
        if (enabled)
        {
            healValue = (int)(healValue * ArcaneHealing.RESTORE_MULT);
        }
    }
    public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
    {
        if (enabled)
        {
            healValue = (int)(healValue * ArcaneHealing.RESTORE_MULT);
        }
    }
}