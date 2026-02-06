using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class FractalizedResetBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetAttackSpeed<RangedDamageClass>() += FractalizedReset.RANGED_FIRE_RATE / 100f;
    }
}
