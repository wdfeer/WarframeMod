using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class VirtuosStrike : Arcane
{
    public const int CHANCE = 15;
    public const float EXTRA_CRIT_MULT = 0.33f;
    public const int BUFF_DURATION = 420;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<VirtuosStrikePlayer>().enabled = true;
    }
}
class VirtuosStrikePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (enabled && proj.DamageType == DamageClass.Magic && hit.Crit && (Main.rand.NextFloat() < VirtuosStrike.CHANCE / 100f))
            Player.AddBuff(ModContent.BuffType<VirtuosStrikeBuff>(), VirtuosStrike.BUFF_DURATION);
    }
}