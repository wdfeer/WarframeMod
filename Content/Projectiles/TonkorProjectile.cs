namespace WarframeMod.Content.Projectiles;
internal class TonkorProjectile : ExplosiveProjectile
{
    public override int ExplosionWidth => 170;
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.height = 20;
        Projectile.width = 20;
        Projectile.timeLeft = 240;
    }
    float rotationSpeed = Main.rand.NextFloat(-1, 1);
    public float gravity = 0.25f;
    public override void AI()
    {
        Projectile.rotation += rotationSpeed;
        Projectile.velocity.Y += gravity;

        var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke)];
        dust.scale = 0.6f;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        switch (target.type)
        {
            case NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail:
                modifiers.SourceDamage /= 2;
                break;
            default:
                return;
        }
    }
}