using Terraria.Audio;
using Terraria.ID;

namespace WarframeMod.Content.Projectiles;
internal abstract class ExplosiveProjectile : ModProjectile
{
    public abstract int ExplosionWidth { get; }
    public virtual int ExplosionHeight => ExplosionWidth;
    public virtual bool CanExplode() => true;
    public virtual bool ExplodeOnTileCollide => true;
    public virtual bool ExplodeOnNPCHit => true;
    public virtual bool ExplodeOnTimeOut => true;
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
    protected bool exploding = false;
    public override bool PreAI()
    {
        if (Projectile.timeLeft <= 2 && ExplodeOnTimeOut && CanExplode() && !exploding)
            Explode();
        return true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (!ExplodeOnTileCollide || !CanExplode())
            return base.OnTileCollide(oldVelocity);
        Explode();
        return false;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (ExplodeOnNPCHit && CanExplode())
            Explode();
    }
    public virtual void Explode()
    {
        exploding = true;

        Projectile.Resize(ExplosionWidth, ExplosionHeight);
        Projectile.timeLeft = 2;
        Projectile.tileCollide = false;

        ExplosionDusts();
        ExplosionSound();
    }
    public virtual void ExplosionDusts()
    {
        for (int i = 0; i < 25; i++)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
            dust.velocity *= 1.3f;
        }
        for (int i = 0; i < 40; i++)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 3f);
            dust.noGravity = true;
            dust.velocity *= 5f;
            dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 2f);
            dust.velocity *= 3f;
        }
    }
    public virtual void ExplosionSound()
        => SoundEngine.PlaySound(SoundID.Item14.WithVolumeScale(0.5f), Projectile.Center);
}
