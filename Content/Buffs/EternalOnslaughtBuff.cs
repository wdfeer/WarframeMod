using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;

public class EternalOnslaughtBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetCritChance(DamageClass.Magic) += EternalOnslaught.CRIT_CHANCE_BONUS;
    }
}
