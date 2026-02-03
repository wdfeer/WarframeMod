namespace WarframeMod.Content.Buffs;
public class LongbowSharpshotBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
