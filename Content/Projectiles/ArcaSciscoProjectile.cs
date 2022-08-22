namespace WarframeMod.Content.Projectiles;

public class ArcaSciscoProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.EmeraldBolt;
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.hide = true;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.timeLeft = 360;
        Projectile.extraUpdates = 9;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
    public override void AI()
    {
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MagnetSphere);
    }
    public Action onHit;
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        => onHit();
    public override void OnHitPlayer(Player target, int damage, bool crit)
        => onHit();
    public override void OnHitPvp(Player target, int damage, bool crit)
        => onHit();
}
