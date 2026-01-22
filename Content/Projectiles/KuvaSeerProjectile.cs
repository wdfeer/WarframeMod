using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

internal class KuvaSeerProjectile : ExplosiveProjectile
{
    const int BASE_BULLET_TYPE = ProjectileID.ExplosiveBullet;
    public override string Texture => "Terraria/Images/Projectile_" + BASE_BULLET_TYPE;
    public override int ExplosionWidth => 120;
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(BASE_BULLET_TYPE);
        AIType = BASE_BULLET_TYPE;

        base.SetDefaults();

        Projectile.DamageType = DamageClass.Ranged;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (exploding)
        {
            modifiers.SourceDamage *= KuvaSeer.EXPLOSION_DAMAGE_PERCENT / 100f;
        }
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 30; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[dustIndex].velocity *= 1f;
        }
    }
}