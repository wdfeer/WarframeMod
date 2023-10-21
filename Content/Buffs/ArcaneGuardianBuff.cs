using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneGuardianBuff : ModBuff
{
    int defense = 1;
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = $"+{defense} Defense";
    }
    public override void Update(Player player, ref int buffIndex)
    {
        defense = player.GetModPlayer<GuardianPlayer>().currentDefense;
        player.statDefense += defense;
    }
}
