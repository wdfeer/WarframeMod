using Terraria.Audio;
using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class GrimoireAltProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;
    
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.scale = 2f;
        Projectile.height = 64;
        Projectile.width = 64;
        Projectile.alpha = 164;
        Projectile.timeLeft = 240;
        Projectile.penetrate = -1;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 20;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(100);
    }

    public override void AI()
    {
        DustHelper.NewDustsCircleEdge(24, Projectile.Center, Projectile.width / 2, DustID.GemAmethyst);
        DustHelper.NewDustsCircleFromCenter(6, Projectile.Center, Projectile.width / 3, DustID.Electric, 2f);
    }
}