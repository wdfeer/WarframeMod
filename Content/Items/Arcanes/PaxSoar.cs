using Terraria.Localization;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Arcanes;

public class PaxSoar : Arcane
{
    public const int DAMAGE_INCREASE = 12;
    public const int WING_BUFF = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DAMAGE_INCREASE, WING_BUFF);
    public override void UpdateArcane(Player player)
    {
        if (player.wingTime < player.wingTimeMax)
        {
            player.GetDamage<GenericDamageClass>() += DAMAGE_INCREASE / 100f;
        }
        player.GetModPlayer<WingSpeedPlayer>().horizontalWingSpeedMult += WING_BUFF / 100f;
    }
}