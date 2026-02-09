namespace WarframeMod.Content.Projectiles;

public class SpiraProjectile : KunaiProjectile
{
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.penetrate = 1;
    }
}