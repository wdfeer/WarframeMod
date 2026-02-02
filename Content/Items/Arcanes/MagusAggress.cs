using Terraria.Localization;
using WarframeMod.Common.Players;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class MagusAggress : Arcane
{
    public const int MELEE_CRIT = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MELEE_CRIT);
    public override void UpdateArcane(Player player)
    {
        if (player.HasBuff(BuffID.ChaosState))
        {
            player.GetCritChance<MeleeDamageClass>() += MELEE_CRIT;
        }
    }
}