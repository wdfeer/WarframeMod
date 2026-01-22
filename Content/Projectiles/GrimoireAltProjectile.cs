using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Projectiles;

public class GrimoireAltProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;

    private const int baseTimeLeft = 180;
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.hide = true;
        Projectile.height = 64;
        Projectile.width = 64;
        Projectile.timeLeft = baseTimeLeft;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 20;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(100);
    }

    public override void AI()
    {
        float dustMultiplier = (float)Projectile.timeLeft / baseTimeLeft;
        DustHelper.NewDustsCircleEdge((int)(9 * dustMultiplier),
            Projectile.Center,
            Projectile.width / 2,
            DustID.GemAmethyst,
            dust => dust.noGravity = true);
        DustHelper.NewDustsCircleFromCenter((int)(3 * dustMultiplier),
            Projectile.Center,
            Projectile.width / 3,
            DustID.Electric,
            2f);
    }
}