namespace WarframeMod.Content.Buffs;
public class ArcaneBodyguardBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
