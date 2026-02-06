using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class FractalizedReset : Arcane
{
    public const int MIN_MANA = 300;
    public const int RANGED_FIRE_RATE = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MIN_MANA, RANGED_FIRE_RATE);

    public override void UpdateArcane(Player player)
    {
        if (player.statMana >= MIN_MANA)
        {
            player.GetAttackSpeed<RangedDamageClass>() += RANGED_FIRE_RATE / 100f;
        }
    }
}