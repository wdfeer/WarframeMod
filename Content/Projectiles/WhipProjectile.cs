namespace WarframeMod.Content.Projectiles;
public abstract class WhipProjectile : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.IsAWhip[Type] = true;
    }
    public override void SetDefaults()
    {
        Projectile.DefaultToWhip();
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
}