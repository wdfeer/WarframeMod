using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneIntention : Arcane
{
    public const int MAX_LIFE_PER_MINION = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MAX_LIFE_PER_MINION);
    public override void UpdateArcane(Player player)
    {
        player.statLifeMax2 += (int)(player.slotsMinions * MAX_LIFE_PER_MINION);
    }
}