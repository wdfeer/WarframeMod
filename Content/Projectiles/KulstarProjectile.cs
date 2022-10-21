using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Projectiles;

internal class KulstarProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RocketII;
    public override int ExplosionWidth => 240;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.timeLeft = 400;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.15f;
    }
    public override void AI()
    {
        if (exploding)
            return;
        if (Projectile.velocity.Y < 16)
            Projectile.velocity.Y += 0.12f;
        for (int num = 0; num < 2; num++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            dust.scale = Main.rand.NextFloat(1f, 2f);
            dust.velocity *= 1.25f;
        }
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
    }
    public override void Explode()
    {
        base.Explode();
        for (int i = 0; i < 3; i++)
        {
            Projectile p = Projectile.NewProjectileDirect(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                Main.rand.NextVector2CircularEdge(16, 16),
                ModContent.ProjectileType<KulstarClusterProjectile>(),
                Projectile.damage / 3,
                Projectile.knockBack / 3,
                Projectile.owner);
        }
    }
    public override float ExplosionSoundVolume => 1f;
}
class KulstarClusterProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Grenade;
    public override int ExplosionWidth => 80;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.hide = true;
        Projectile.width = 16;
        Projectile.height = 16;
    }
    public override void AI()
    {
        if (exploding)
            return;
        if (Projectile.velocity.Y < 16)
            Projectile.velocity.Y += 0.6f;
        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
        dust.scale = Main.rand.NextFloat(0.9f, 1.35f);
        dust.velocity *= 0.6f;
    }
}