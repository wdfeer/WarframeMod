using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace WarframeMod.Projectiles
{
    internal class FluxRifleProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DiamondBolt;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.height = 12;
            Projectile.width = 12;
            Projectile.hide = true;
            Projectile.penetrate = 2;
            Projectile.extraUpdates = 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ArmorPenetration = 10;
        }
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 position = Projectile.position;
                position -= Projectile.velocity * Main.rand.NextFloat(-0.25f, 0.25f);
                var dust = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, DustID.GemDiamond, Scale: 0.8f)];
                dust.velocity *= 0;
                dust.noGravity = true;
            }
        }
    }
}