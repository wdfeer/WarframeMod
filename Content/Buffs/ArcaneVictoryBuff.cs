using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneVictoryBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Victory");
        Description.SetDefault($"+{ArcaneVictory.LIFE_REGEN * 100}% max life per second");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
