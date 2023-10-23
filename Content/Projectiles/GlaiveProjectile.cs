namespace WarframeMod.Content.Projectiles;

public abstract class GlaiveProjectile : ExplosiveProjectile
{
    public override bool ExplodeOnNPCHit => false;
    public override bool ExplodeOnTileCollide => false;
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.ThornChakram);
        base.SetDefaults();
        Projectile.DamageType = DamageClass.Melee;
    }
}