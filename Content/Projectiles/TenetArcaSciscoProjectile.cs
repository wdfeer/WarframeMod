using WarframeMod.Common;

namespace WarframeMod.Content.Projectiles;

public class TenetArcaSciscoProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.EmeraldBolt;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.hide = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.timeLeft = 360;
        Projectile.extraUpdates = 9;
    }
    public override void AI()
    {
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MagnetSphere);
    }
    public Action onHit;
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(target, hit, damageDone);
        onHit();
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        base.OnHitPlayer(target, info);
        onHit();
    }
    
    public override int ExplosionWidth => 40;
    public override void ExplosionSound() { }
    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCircleEdge(16, Projectile.Center, Projectile.width / 2, DustID.MagnetSphere);
    }
}
