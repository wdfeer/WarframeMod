using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class CascadiaOvercharge : Arcane
{
    public const int CRIT_CHANCE = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRIT_CHANCE}%");
    
    public override void UpdateArcane(Player player)
    {
        if (player.statLife >= player.statLifeMax2)
        {
            player.GetCritChance<GenericDamageClass>() += CRIT_CHANCE;
        }
    }
}