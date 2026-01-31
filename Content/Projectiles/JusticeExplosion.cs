namespace WarframeMod.Content.Projectiles;

public class JusticeExplosion : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.None;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.hide = true;
        Projectile.usesLocalNPCImmunity = false;
        Projectile.localNPCHitCooldown = -2;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 20;
    }

    public override int ExplosionWidth => 2 * 16;
    public override void AI()
    {
        base.AI();
        if (!exploding)
            Explode();
    }
}