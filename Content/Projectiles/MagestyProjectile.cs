using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class MagestyProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AbigailCounter;
    public override int ExplosionWidth => 160;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.hide = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBleed(Magesty.BLEED_CHANCE);
        Projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier +=
            Magesty.CRIT_DAMAGE_BONUS_PERCENT / 100f;
    }

    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCircleEdge(30,
            Projectile.Center,
            ExplosionWidth / 2f,
            DustID.Clentaminator_Cyan,
            d => d.noGravity = true);
        DustHelper.NewDustsCircleFromCenter(27,
            Projectile.Center,
            ExplosionWidth / 2f,
            DustID.Clentaminator_Cyan,
            1f,
            d => d.noGravity = true);
    }
}