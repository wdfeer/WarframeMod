using WarframeMod.Content.Items.Consumables;

namespace WarframeMod.Content.Buffs;
public class LohkCanticleBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetAttackSpeed(DamageClass.Generic) += LohkCanticle.FIRE_RATE_INCREASE_PERCENT / 100f;
    }
}
