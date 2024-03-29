using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneStrikeBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetAttackSpeed(DamageClass.Melee) += ArcaneStrike.SPEED_BUFF / 100f;
    }
}
