using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod.Projectiles
{
    internal class QuassusProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DiamondBolt;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.timeLeft = 60;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.height = 8;
            Projectile.width = 8;
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Titanium, Scale: 0.69f)];
                dust.noGravity = true;
            }
        }
    }
}