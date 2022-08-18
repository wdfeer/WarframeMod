using WarframeMod.Common.Players;

namespace WarframeMod.Common.GlobalItems;
internal class FireRateGlobalItem : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override float UseSpeedMultiplier(Item item, Player player)
    {
        return player.GetModPlayer<FireRatePlayer>().FireRateMultiplier;
    }
}
