using Humanizer;
using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneAgilityBuff : ModBuff
{
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip.FormatWith(ArcaneAgility.MOVE_SPEED, ArcaneAgility.FLIGHT_SPEED);
    }

    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.moveSpeed += ArcaneAgility.MOVE_SPEED / 100f;
        var wingSpeedPlayer = player.GetModPlayer<WingSpeedPlayer>();
        wingSpeedPlayer.verticalWingSpeedMult += ArcaneAgility.FLIGHT_SPEED / 100f;
        wingSpeedPlayer.horizontalWingSpeedMult += ArcaneAgility.FLIGHT_SPEED / 100f;
    }
}
