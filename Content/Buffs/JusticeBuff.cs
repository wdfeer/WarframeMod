using WarframeMod.Content.Items.Consumables;

namespace WarframeMod.Content.Buffs;

public class JusticeBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense += ShatteringJustice.EFFECT_DEFENSE_INCREASE;
    }
}