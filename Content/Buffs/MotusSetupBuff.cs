using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Content.Buffs;

public class MotusSetupBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<CritPlayer>().relativeCritChance += MotusSetup.RELATIVE_CRIT_PERCENT / 100f;
    }
}
