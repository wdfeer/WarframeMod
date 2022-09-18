using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class VirtuosStrikeBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Virtuos Strike");
        Description.SetDefault($"+{(int)(VirtuosStrike.EXTRA_CRIT_MULT * 100)}% Critical Damage");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<CritPlayer>().critMultiplierPlayer += VirtuosStrike.EXTRA_CRIT_MULT;
    }
}
