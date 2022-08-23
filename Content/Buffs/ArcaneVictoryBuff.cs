using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Content.Buffs;
public class ArcaneVictoryBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Victory");
        Description.SetDefault($"+{ArcaneVictory.LIFE_REGEN}% max life per second");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
