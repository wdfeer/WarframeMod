using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class KunaiProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.width = 31;
        Projectile.height = 31;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBleed(Kunai.BLEED_CHANCE);
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = 1;
        height = 1;
        hitboxCenterFrac = Vector2.Zero;
        return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public override void AI()
    {
        const float gravity = 12f / 60f;
        if (Projectile.velocity.Y < 30f)
        {
            Projectile.velocity.Y += gravity;
        }

        Projectile.rotation = -MathF.Atan2(Projectile.velocity.X, Projectile.velocity.Y);
    }
}