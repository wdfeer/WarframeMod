using WarframeMod.Common;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Projectiles;

public class ResidualBoilsProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.None;

    public override void SetDefaults()
    {
        Projectile.width = ResidualBoils.AOE_RADIUS * 2;
        Projectile.height = ResidualBoils.AOE_RADIUS * 2;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.hide = true;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.timeLeft = ResidualShock.DURATION;
        Projectile.penetrate = -1;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 20;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        DustHelper.NewDustsCircleEdge(2, Projectile.Center, ResidualBoils.AOE_RADIUS, DustID.FlameBurst);
        DustHelper.NewDustsCircleEdge(20, Projectile.Center, ResidualBoils.AOE_RADIUS, DustID.Torch,
            d =>
            {
                d.noGravity = true;
                d.velocity *= 0f;
            });
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 300);
    }
}