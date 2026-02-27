using Humanizer;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;

public class ArcaneCamisadoBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    int stacks;
    public override void Update(Player player, ref int buffIndex)
    {
        stacks = player.GetModPlayer<ArcaneCamisadoPlayer>().stacks;
    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = tip.FormatWith(stacks * ArcaneCamisado.MAGIC_DAMAGE_PER_PROC);
    }
}
