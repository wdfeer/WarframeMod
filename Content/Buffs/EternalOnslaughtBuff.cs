using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;

public class EternalOnslaughtBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Eternal Onslaught");
        Description.SetDefault($"+{EternalOnslaught.CRIT_CHANCE_BONUS}% magic crit chance");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetCritChance(DamageClass.Magic) += EternalOnslaught.CRIT_CHANCE_BONUS;
    }
}
