namespace WarframeMod.Content.Buffs;
public class ArcanePistoleerBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
