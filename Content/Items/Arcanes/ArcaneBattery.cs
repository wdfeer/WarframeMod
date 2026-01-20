using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneBattery : Arcane
{
    public const int MANA_PER_DEFENSE = 3;
    public const int MAX_EXTRA_MANA = 120;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MANA_PER_DEFENSE, MAX_EXTRA_MANA);
    public override void UpdateArcane(Player player)
    {
        var increase = MANA_PER_DEFENSE * player.statDefense;
        increase = Math.Min(increase, MAX_EXTRA_MANA);
        player.statManaMax2 += increase;
    }
}