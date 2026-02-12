using WarframeMod.Common;

namespace WarframeMod.Content.Projectiles;

public class SpiraProjectile : KunaiProjectile
{
    public override void SetDefaults()
    {
        Projectile.DamageType = Calamity.Throwing;
        Projectile.friendly = true;
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.penetrate = 1;
    }
}