using Terraria.Localization;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneAcceleration : Arcane
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CHANCE, USE_SPEED_BUFF, BUFF_DURATION / 60);
    public const int CHANCE = 25;
    public const int USE_SPEED_BUFF = 15;
    public const int BUFF_DURATION = 60 * 9;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneAccelerationPlayer>().enabled = true;
    }
}
class ArcaneAccelerationPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        if (Main.rand.Next(0, 100) < ArcaneAcceleration.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneAccelerationBuff>(), ArcaneAcceleration.BUFF_DURATION);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit) ApplyBuff();
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit) ApplyBuff();
    }
}