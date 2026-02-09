using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class MagusVigor : Arcane
{
    public const int MAX_LIFE_FLAT = 60;
    public const int LIFE_REGEN_PER_SECOND = 1;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MAX_LIFE_FLAT);

    public override void UpdateArcane(Player player)
    {
        player.statLifeMax2 += MAX_LIFE_FLAT;
        player.lifeRegen += LIFE_REGEN_PER_SECOND * 2;
    }
}