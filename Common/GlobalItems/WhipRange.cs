using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Common.GlobalItems;

public class WhipRange : GlobalItem
{
    public override bool InstancePerEntity => true;
    public static float GetWhipExtraRange(float additiveRange) => additiveRange / 300f;
    public float extraRangeMult = 1f;
    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (item.DamageType != DamageClass.SummonMeleeSpeed)
            return;
        var rangePl = player.GetModPlayer<TrueMeleeRangePlayer>();
        float rangeMult = rangePl.rangeMult + GetWhipExtraRange(rangePl.absoluteExtraRange) * extraRangeMult;
        if (rangeMult > 2.5f)
            rangeMult = 2.5f;
        velocity *= rangeMult;
    }
}
