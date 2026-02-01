using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class ExodiaForceProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AbigailCounter;
    public override int ExplosionWidth => 160;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.hide = true;
        Projectile.DamageType = DamageClass.Melee;
    }

    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCircleEdge(30,
            Projectile.Center,
            ExplosionWidth / 2f,
            DustID.Clentaminator_Red,
            d => d.noGravity = true);
        DustHelper.NewDustsCircleFromCenter(27,
            Projectile.Center,
            ExplosionWidth / 2f,
            DustID.FireworkFountain_Red,
            1f,
            d => d.noGravity = true);
    }
}