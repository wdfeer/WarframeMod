using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class MoltEfficiency : Arcane
{
    public const int MANA_USAGE_REDUCTION = 30;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MANA_USAGE_REDUCTION);
    
    public override void UpdateArcane(Player player)
    {
        if (player.statLife >= player.statLifeMax2)
        {
            player.manaCost *= (1f - MANA_USAGE_REDUCTION / 100f);
        }
    }
}