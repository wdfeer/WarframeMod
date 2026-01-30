using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Projectiles;

public class SecuraLectaProjectile : LectaProjectile
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().buffChances.Add(
            new BuffChance(BuffID.Midas, 298, 1f));
    }

    public override void AI()
    {
        base.AI();
        Lighting.AddLight(Projectile.Center, 0.24f, 0.82f, 0.84f);
    }
}