namespace WarframeMod.Content.Projectiles;

internal class OrviusProjectile : GlaiveProjectile
{
    public override string Texture => "WarframeMod/Content/Items/Weapons/Orvius";
    public override int ExplosionWidth => 240;
    public override void AI()
    {
        if (exploding)
            return;
        Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple);
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 80; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple);
            dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * 2.5f;
        }
    }
}