using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class EternalLogistics : Arcane
{
    public const int MANA_USAGE_REDUCTION = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MANA_USAGE_REDUCTION);
    public override void UpdateArcane(Player player)
    {
        if (!player.HasBuff(BuffID.ManaSickness))
        {
            player.manaCost *= (100 - MANA_USAGE_REDUCTION) / 100f;
        }
    }
}