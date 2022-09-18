using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneGuardianBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Guardian");
        Description.SetDefault("+1 Defense");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    int defense = 1;
    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        tip = $"+{defense} Defense";
    }
    public override void Update(Player player, ref int buffIndex)
    {
        defense = player.GetModPlayer<GuardianPlayer>().currentDefense;
        player.statDefense += defense;
    }
}
