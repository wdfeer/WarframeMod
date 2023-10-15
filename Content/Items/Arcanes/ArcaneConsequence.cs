using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneConsequence : Arcane
{
    public const int PERCENT_MOVEMENT_SPEED_INCREASE = 20;
    public const int PERCENT_WING_SPEED_INCREASE = 12;
    public const int DURATION = 360;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ConsequencePlayer>().enabled = true;
    }
}
class ConsequencePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    void OnHit(bool crit)
    {
        if (enabled && crit)
            Player.AddBuff(ModContent.BuffType<ArcaneConsequenceBuff>(), ArcaneConsequence.DURATION);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        => OnHit(hit.Crit);
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        => OnHit(crit);
    public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        => OnHit(crit);
    public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
        => OnHit(crit);
}