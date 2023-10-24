namespace WarframeMod.Content.Projectiles;
internal class ArcaPlasmorProjectile : ModProjectile
{
    public virtual bool Tenet => false;
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Magic;
        Projectile.width = 80;
        Projectile.height = 80;
        Projectile.scale = 0.6f;
        Projectile.alpha = 128;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 32;
        Projectile.light = 0.5f;
        Projectile.tileCollide = false;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
    public override void AI()
    {
        Projectile.rotation = Convert.ToSingle(-Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y));
        for (int i = 0; i < (Tenet ? 2 : 1); i++)
        {
            var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226)];
            dust.scale = 0.75f;
            Projectile.alpha = 256 - Projectile.timeLeft * (Tenet ? 3 : 4);
            Projectile.light = Projectile.timeLeft * 0.01f;
        }

        if (TileColliding())
        {
            for (int i = 0; i < 16; i++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 180, 0f, 0f, 75, default, 0.6f);
            }
            Projectile.timeLeft = 1;
        }
    }
    bool TileColliding()
    {
        Vector2 lineStart = Projectile.Center - Projectile.velocity * 0.5f;
        Vector2 lineEnd = Projectile.Center + Projectile.velocity * 0.5f;
        return !Collision.CanHitLine(lineStart, 0, 0, lineEnd, 0, 0);
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (Projectile.timeLeft > 12)
        {
            if (Tenet) Projectile.timeLeft -= 8;
            else
            {
                Projectile.timeLeft = 12;
                Projectile.velocity *= 0.6f;
            }
        }
        for (int i = 0; i < 24; i++)
        {
            Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 180, 0f, 0f, 75, default, 0.6f);
        }
    }
}