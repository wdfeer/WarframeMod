using WarframeMod.Content.Items.Consumables;

namespace WarframeMod.Content.Buffs;
public class VomeInvocationBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    int stacks = 0;
    public override void Update(Player player, ref int buffIndex)
    {
        stacks = player.GetModPlayer<VomeInvocationPlayer>().stacks;
    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = $"+{4 * stacks}% Damage on the Grimoire";
    }

}
