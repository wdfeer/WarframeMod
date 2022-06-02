using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod.Projectiles
{
    internal class MagnetizeProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DiamondBolt;
        public NPC target;
        public override void SetDefaults()
        {
            Projectile.hide = true;
            Projectile.width = 240;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            if (target == null || !target.active)
            {
                Projectile.timeLeft = 0;
                return;
            }
            Projectile.Center = target.Center;

            Projectile[] projectiles = GetValidIntersectingProjectiles();
            foreach (var proj in projectiles)
            {
                proj.velocity += Vector2.Normalize(Projectile.Center - proj.Center);
            }

            SpawnDusts();
        }
        public override bool ShouldUpdatePosition() => false;
        Projectile[] GetValidIntersectingProjectiles()
        {
            return Main.projectile.Where(x =>
            {
                return x.active
                       && x.friendly
                       && !Main.projPet[x.type]
                       && x.Center.Distance(Projectile.Center) < Projectile.width;
            }).ToArray();
        }
        void SpawnDusts()
        {
            for (int i = 0; i < Projectile.width / 8; i++)
            {
                Vector2 position = Projectile.Center + Main.rand.NextVector2CircularEdge(Projectile.width, Projectile.width);
                Dust d = Dust.NewDustDirect(position, 1, 1, DustID.MagnetSphere);
                d.noGravity = true;
            }
        }
    }
}