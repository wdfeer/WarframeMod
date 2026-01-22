namespace WarframeMod.Content.Projectiles;
internal class LenzProjBubble : ExplosiveProjectile
{
    public override int ExplosionWidth => 320;
    public override bool ExplodeOnNPCHit => false;
    public override bool ExplodeOnTileCollide => false;
    public override bool ExplodeOnTimeOut => true;
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.width = 320;
        Projectile.height = 320;
        Projectile.scale = 1f;
        Projectile.alpha = 196;
        Projectile.tileCollide = false;
        Projectile.knockBack = 12;
        Projectile.timeLeft = 78;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
    NPC impaledNPC;
    Vector2 impaleOffset;
    public override void AI()
    {
        if (Projectile.timeLeft <= 16) Projectile.alpha = 256 - Projectile.timeLeft * 4;
        else
        {
            int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard, 0f, 0f, 80, default(Color), 1.2f);
            var dust = Main.dust[dustIndex];
            dust.noGravity = true;
            dust.velocity *= 0.75f;
        }

        if (impaledNPC != null && impaledNPC.active)
        {
            Projectile.position = impaledNPC.Center - new Vector2(Projectile.width / 2, Projectile.height / 2) + impaleOffset;
        }
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 40; i++)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
            dust.velocity *= 1.3f;
        }
        for (int i = 0; i < 25; i++)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 3f);
        }
        for (int i = 0; i < 100; i++)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 3f);
            dust.noGravity = true;
            dust.velocity *= 5f;
            dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 2f);
            dust.velocity *= 3f;
        }
    }
    public override bool? CanHitNPC(NPC target)
    {
        if (Projectile.timeLeft > 4) return false; else return null;
    }
    public void Impale(NPC npc, Vector2 offset)
    {
        impaledNPC = npc;
        impaleOffset = offset;
    }
}