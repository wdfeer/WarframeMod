namespace WarframeMod.Content.Projectiles;
internal class TenetEnvoyProjectile : ExplosiveProjectile
{
    public override int ExplosionWidth => 340;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.height = 22;
        Projectile.width = 22;
        Projectile.timeLeft = 600;
    }
    public override void AI()
    {
        var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, Scale: 1.2f)];

        if (Main.myPlayer != Projectile.owner)
            return;

        Vector2 diff = Main.MouseWorld - Projectile.position;
        Projectile.velocity = Vector2.Normalize(diff);
        Projectile.position += Math.Min(10, diff.Length()) * Projectile.velocity;

        float rotation = (float)(Math.PI / 2 + -Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y));
        Projectile.rotation = rotation;

        Projectile.netUpdate = true;
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 50; i++)
        {
            var dust = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * Projectile.width / 4, 206, Scale: 1.75f);
            dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * 4;
        }
        for (int i = 0; i < 50; i++)
        {
            var dust = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * Projectile.width / 4, 206, Scale: 1.75f);
            dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * 10;
        }
    }
}