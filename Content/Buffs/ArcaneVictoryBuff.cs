using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneVictoryBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
