using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcanePistoleerBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Pistoleer");
        Description.SetDefault($"+{(int)(ArcanePistoleer.AMMO_DAMAGE_INCREASE * 100)}% ammo damage");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
