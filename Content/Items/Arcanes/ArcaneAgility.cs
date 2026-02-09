using Terraria.Localization;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneAgility : Arcane
{
    public const int MOVE_SPEED = 10;
    public const int FLIGHT_SPEED = 20;
    public const int BUFF_DURATION_SECONDS = 30;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(MOVE_SPEED, FLIGHT_SPEED, BUFF_DURATION_SECONDS);

    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneAgilityPlayer>().enabled = true;
    }
}

class ArcaneAgilityPlayer : ModPlayer
{
    public bool enabled;

    public override void ResetEffects()
        => enabled = false;

    public override void OnHurt(Player.HurtInfo info)
    {
        if (enabled)
            Player.AddBuff(ModContent.BuffType<ArcaneAgilityBuff>(), ArcaneAgility.BUFF_DURATION_SECONDS * 60);
    }
}