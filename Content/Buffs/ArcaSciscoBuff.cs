using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Buffs;

public class ArcaSciscoBuff : ModBuff
{
    int stacks = 0;
    public override void Update(Player player, ref int buffIndex)
    {
        stacks = player.GetModPlayer<ArcaSciscoPlayer>().stacks;
    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = $"+{5 * stacks}% Crit and Slash chance on the Arca Scisco";
    }
}
