using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class MagusCadence : Arcane
{
    public const int MOVEMENT_SPEED = ArcaneConsequence.PERCENT_MOVEMENT_SPEED_INCREASE + 15;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MOVEMENT_SPEED);
    public override void UpdateArcane(Player player)
    {
        if (player.HasBuff(BuffID.ChaosState))
        {
            player.moveSpeed += MOVEMENT_SPEED / 100f;
        }
    }
}