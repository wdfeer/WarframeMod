namespace WarframeMod.Common.Players;

class WingSpeedPlayer : ModPlayer
{
    public float verticalWingSpeedMult = 1f;
    public float horizontalWingSpeedMult = 1f;
    public override void ResetEffects()
    {
        verticalWingSpeedMult = 1f;
        horizontalWingSpeedMult = 1f;
    }
}
class WingSpeedGlobalItem : GlobalItem
{
    public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        maxAscentMultiplier *= player.GetModPlayer<WingSpeedPlayer>().verticalWingSpeedMult;
    }
    public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
    {
        speed *= player.GetModPlayer<WingSpeedPlayer>().horizontalWingSpeedMult;
    }
}