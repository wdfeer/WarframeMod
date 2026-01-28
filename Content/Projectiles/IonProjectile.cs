using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Projectiles;

internal class IonProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.StarCannonStar;
    public override int ExplosionWidth => 4 * 16 * 2;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.StarCannonStar);
        base.SetDefaults();
        Projectile.DamageType = DamageClass.Melee;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(100);
    }

    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCircleFromCenter(10,
            Projectile.Center,
            Projectile.width / 2f,
            DustID.Electric,
            2f,
            d => d.noGravity = true);
        DustHelper.NewDustsCircleFromCenter(10,
            Projectile.Center,
            Projectile.width / 2f,
            DustID.YellowStarDust,
            2f);
    }
}