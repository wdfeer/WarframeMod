namespace WarframeMod.Content.Projectiles;

internal class AcceltraProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;
    public override int ExplosionWidth => 200;
    public override bool CanExplode()
    {
        return initialPos.Distance(Projectile.position) > ExplosionWidth;
    }
    public Vector2 initialPos;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.hide = true;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.timeLeft = 360;
        Projectile.DamageType = DamageClass.Ranged;
    }
    public override void AI()
    {
        if (exploding)
            return;
        for (int num = 0; num < 2; num++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.WhiteTorch);
            dust.noLight = true;
            dust.scale = Main.rand.Next(80, 130) * 0.01f;
            dust.velocity *= 0.2f;
            dust.noGravity = true;
        }
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (!CanExplode())
        {
            modifiers.Knockback *= 2;
            modifiers.SourceDamage /= 2;
            Projectile.penetrate = 0;
        }
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 30; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.AncientLight);
            dust.scale = 1.5f;
            dust.noGravity = true;
            dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * 8;
        }
    }
}