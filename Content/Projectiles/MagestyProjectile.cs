using WarframeMod.Common;

namespace WarframeMod.Content.Projectiles;

public class MagestyProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SapphireBolt;
    public override int ExplosionWidth => 64;
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.SapphireBolt);
        AIType = ProjectileID.SapphireBolt;
        base.SetDefaults();
        Projectile.extraUpdates = 3;
    }

    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCircleEdge(11,
            Projectile.Center,
            ExplosionWidth / 2f,
            DustID.GemSapphire,
            d => d.noGravity = true);
    }
}