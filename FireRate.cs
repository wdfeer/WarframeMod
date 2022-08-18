namespace WarframeMod;

internal class FireRatePlayer : ModPlayer
{
    public float FireRateMultiplier { get; set; }
    public override void ResetEffects()
    {
        FireRateMultiplier = 1;
    }
}
internal class FireRateGlobalItem : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override float UseSpeedMultiplier(Item item, Player player)
    {
        return player.GetModPlayer<FireRatePlayer>().FireRateMultiplier;
    }
}
