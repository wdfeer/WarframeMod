namespace WarframeMod.Common.Players;

internal class FireRatePlayer : ModPlayer
{
    public float TotalFireRateMultiplier { get; set; }
    public float RangedFireRateMult { get; set; }
    public float MagicFireRateMult { get; set; }
    public override void ResetEffects()
    {
        TotalFireRateMultiplier = 1;
        RangedFireRateMult = 1;
        MagicFireRateMult = 1;
    }
    float GetItemFireRateMult(Item item)
    {
        float mult = 1;
        if (item.DamageType == DamageClass.Magic)
            mult = MagicFireRateMult;
        else if (item.DamageType == DamageClass.Ranged)
            mult = RangedFireRateMult;
        return TotalFireRateMultiplier * mult;
    }
    public override float UseSpeedMultiplier(Item item)
    {
        return GetItemFireRateMult(item);
    }
}
