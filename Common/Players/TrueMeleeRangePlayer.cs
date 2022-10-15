namespace WarframeMod.Common.Players;

class TrueMeleeRangePlayer : ModPlayer
{
    public int absoluteExtraRange;
    public float rangeMult;
    public override void ResetEffects()
    {
        absoluteExtraRange = 0;
        rangeMult = 1f;
    }
}