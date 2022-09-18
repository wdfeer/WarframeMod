using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneConsequenceBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Consequence");
        Description.SetDefault($"+{ArcaneConsequence.PERCENT_MOVEMENT_SPEED_INCREASE}% movement speed and +{ArcaneConsequence.PERCENT_WING_SPEED_INCREASE}% wing flight speed");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.moveSpeed += ArcaneConsequence.PERCENT_MOVEMENT_SPEED_INCREASE / 100f;
        var wingSpeedPlayer = player.GetModPlayer<WingSpeedPlayer>();
        wingSpeedPlayer.verticalWingSpeedMult += ArcaneConsequence.PERCENT_WING_SPEED_INCREASE / 100f;
        wingSpeedPlayer.horizontalWingSpeedMult += ArcaneConsequence.PERCENT_WING_SPEED_INCREASE / 100f;
    }
}
