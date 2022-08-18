namespace WarframeMod.Common.Players;

internal class FireRatePlayer : ModPlayer
{
    public float FireRateMultiplier { get; set; }
    public override void ResetEffects()
    {
        FireRateMultiplier = 1;
    }
}
