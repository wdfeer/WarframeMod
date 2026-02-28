using Humanizer;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Buffs;

public class ArcaSciscoBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    int stacks = 0;
    public override void Update(Player player, ref int buffIndex)
    {
        stacks = player.GetModPlayer<ArcaSciscoPlayer>().stacks;
    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = tip.FormatWith(stacks * 5);
    }
}
