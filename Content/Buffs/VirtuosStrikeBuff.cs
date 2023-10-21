using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class VirtuosStrikeBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<CritPlayer>().critMultiplierPlayer += VirtuosStrike.EXTRA_CRIT_MULT;
    }
}
