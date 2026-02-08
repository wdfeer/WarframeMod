using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class DespairProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.width = 31;
        Projectile.height = 31;
        Projectile.penetrate = 2;
        Projectile.extraUpdates = 1;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBuff(new BuffChance(
            BuffID.CursedInferno, 300, Despair.CURSED_FLAMES_CHANCE));
    }

    public override void AI()
    {
        Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.CursedTorch);
        d.noGravity = true;
        d.velocity *= 0;
        
        const float gravity = 8f / 60f;
        if (Projectile.velocity.Y < 30f)
        {
            Projectile.velocity.Y += gravity;
        }

        Projectile.rotation = -MathF.Atan2(Projectile.velocity.X, Projectile.velocity.Y);
    }
}