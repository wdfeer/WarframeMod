using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Projectiles;
internal class ArcaneEruptionProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;
    public override int ExplosionWidth => ArcaneEruption.RANGE * 2;
    public override bool ExplodeOnNPCHit => false;
    public override bool ExplodeOnTileCollide => false;
    public override bool ExplodeOnTimeOut => true;
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Magic;
        Projectile.friendly = true;
        Projectile.width = 1;
        Projectile.height = 1;
        Projectile.hide = true;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 1;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 75; i++)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
            dust.velocity *= 1.3f;
        }
        for (int i = 0; i < 120; i++)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 3f);
            dust.noGravity = true;
            dust.velocity *= 5f;
            dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 2f);
            dust.velocity *= 3f;
        }
    }
}