using Terraria.Localization;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class FractalizedReset : Arcane
{
    public const int RANGED_FIRE_RATE = 25;
    public const int DURATION_SECONDS = 8;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RANGED_FIRE_RATE, DURATION_SECONDS);

    public override void UpdateArcane(Player player)
    {
        if (player.statMana <= 10 && player.statManaMax2 > 10)
        {
            player.AddBuff(ModContent.BuffType<FractalizedResetBuff>(), DURATION_SECONDS * 60);
        }
    }
}