using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Content.Buffs;

public class CatsEyeBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Cat's Eye");
        Description.SetDefault($"+{CatsEye.CRIT} summon Critical Chance");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<CritsPlayer>().summonCritChance += 50;
    }
}
