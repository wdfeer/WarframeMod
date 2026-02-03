using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class FluxRifleProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.None;
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.hide = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.timeLeft = 360;
        Projectile.extraUpdates = 10;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBleed(FluxRifle.BLEED_CHANCE);
    }
    public override void AI()
    {
        Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.MagnetSphere);
        d.velocity *= 0.1f;
    }
}
