using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Content.Buffs;
public class ArcaneAvengerBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Avenger");
        Description.SetDefault("+1% Critical Chance");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    int critChance = 1;
    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        tip = $"+{critChance}% Critical Chance";
    }
    public override void Update(Player player, ref int buffIndex)
    {
        critChance = player.GetModPlayer<AvengerPlayer>().currentCritChance;
        player.GetCritChance(DamageClass.Generic) += critChance;
    }
}
