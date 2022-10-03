using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcanePrecisionBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Precision");
        Description.SetDefault($"+{ArcanePrecision.DAMAGE_BUFF * 100}% Damage");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Generic) += ArcanePrecision.DAMAGE_BUFF;
    }
}
