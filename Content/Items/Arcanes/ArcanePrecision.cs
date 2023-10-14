using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcanePrecision : Arcane
{
    public const int CHANCE = 20;
    public const float DAMAGE_BUFF = 0.15f;
    public const int BUFF_DURATION = 960;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcanePrecisionPlayer>().enabled = true;
    }
}
class ArcanePrecisionPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ApplyBuff(Projectile proj, bool crit)
    {
        if (enabled && proj.DamageType == DamageClass.Ranged && crit && Main.rand.Next(0, 100) < ArcanePrecision.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcanePrecisionBuff>(), ArcanePrecision.BUFF_DURATION);
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        ApplyBuff(proj, crit);
    }
    public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
    {
        ApplyBuff(proj, crit);
    }
}