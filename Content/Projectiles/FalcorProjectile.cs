namespace WarframeMod.Content.Projectiles;

internal class FalcorProjectile : GlaiveProjectile
{
    public override string Texture => "WarframeMod/Content/Items/Weapons/Falcor";
    public override int ExplosionWidth => 320;
    public override void AI()
    {
        if (exploding)
            return;
        for (int num = 0; num < 2; num++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            dust.noLight = true;
            dust.scale = Main.rand.Next(80, 130) * 0.01f;
            dust.velocity *= 0.2f;
            dust.noGravity = true;
        }
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 30; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            dust.scale = 1.5f;
            dust.noGravity = true;
            dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * 6;

        }
    }
}