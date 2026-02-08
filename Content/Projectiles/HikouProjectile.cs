namespace WarframeMod.Content.Projectiles;

internal class HikouProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.width = 32;
        Projectile.height = 38;
        Projectile.timeLeft = 360;
        Projectile.rotation = Main.rand.NextFloat() * MathF.PI;
    }

    public override void AI()
    {
        const float gravity = 16f / 60f;
        if (Projectile.velocity.Y < 32f)
        {
            Projectile.velocity.Y += gravity;
        }

        Projectile.rotation += MathF.PI * 3f / 60f * MathF.Sign(Projectile.velocity.X);
    }
}