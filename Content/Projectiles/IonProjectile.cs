using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Projectiles;
internal class IonProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.StarCannonStar;
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.StarCannonStar);
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(100);
    }
}