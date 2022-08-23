using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Content.Buffs;
public class ArcaneBodyguardBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Bodyguard");
        Description.SetDefault($"+{ArcaneBodyguard.DAMAGE_REDUCTION}% Damage Reduction");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
}
