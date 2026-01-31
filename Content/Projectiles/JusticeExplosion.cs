namespace WarframeMod.Content.Projectiles;

public class JusticeExplosion : ExplosiveProjectile
{
    public override int ExplosionWidth => 5 * 16;
    public override void AI()
    {
        base.AI();
        if (!exploding)
            Explode();
    }
}