using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneAvengerBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    int critChance = 1;
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = $"+{critChance}% Critical Chance";
    }
    public override void Update(Player player, ref int buffIndex)
    {
        critChance = player.GetModPlayer<AvengerPlayer>().currentCritChance;
        player.GetCritChance(DamageClass.Generic) += critChance;
    }
}
