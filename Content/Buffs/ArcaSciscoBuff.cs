using Terraria;
using Terraria.ModLoader;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Buffs;

public class ArcaSciscoBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arca Scisco");
        Description.SetDefault("+5% Crit and Slash chance on the Arca Scisco");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    int stacks = 0;
    public override void Update(Player player, ref int buffIndex)
    {
        stacks = player.GetModPlayer<ArcaSciscoPlayer>().stacks;
    }
    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        tip = $"+{5 * stacks}% Crit and Slash chance on the Arca Scisco";
    }
}
