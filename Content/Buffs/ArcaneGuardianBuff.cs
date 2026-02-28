using Humanizer;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneGuardianBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    int defense = 1;
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = tip.FormatWith(defense);
    }
    public override void Update(Player player, ref int buffIndex)
    {
        defense = player.GetModPlayer<GuardianPlayer>().currentDefense;
        player.statDefense += defense;
    }
}
