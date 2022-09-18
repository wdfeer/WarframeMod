using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcanePrecision : Arcane
{
    public const int CHANCE = 20;
    public static float DamageBuff => Main.hardMode ? DAMAGE_BUFF_HARDMODE : DAMAGE_BUFF_PREHARDMODE;
    public const float DAMAGE_BUFF_PREHARDMODE = 0.1f;
    public const float DAMAGE_BUFF_HARDMODE = 0.16f;
    public const int BUFF_DURATION = 960;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On ranged Critical hit: {CHANCE}% chance for +{(int)(DAMAGE_BUFF_PREHARDMODE * 100)}% Damage (+{(int)(DAMAGE_BUFF_HARDMODE * 100)}% in Hardmode) for {BUFF_DURATION / 60} seconds");
    }
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