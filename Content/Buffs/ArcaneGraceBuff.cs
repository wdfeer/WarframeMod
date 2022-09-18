using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneGraceBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Grace");
        Description.SetDefault($"+{ArcaneGrace.LIFE_REGEN * 100}% max life per second");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
