using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WarframeMod.Content.Projectiles;

internal class KuvaBrammaProjectile : ExplosiveProjectile
{
    public override int ExplosionWidth => 360;
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
        base.SetDefaults();
        Projectile.height = 32;
        Projectile.width = 32;
    }
    public override void AI()
    {
        var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Red)];
        dust.scale = 0.6f;
    }
    public override void Explode()
    {
        base.Explode();

        for (int i = 0; i < 3; i++)
        {
            var proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.position + new Vector2(Main.rand.Next(0, 240) + Projectile.width / 2, Main.rand.Next(0, 240) + Projectile.height / 2), Vector2.Zero, ModContent.ProjectileType<TonkorProjectile>(), Projectile.damage / 4, Projectile.knockBack / 4, Projectile.owner);
        }
    }
    public override void ExplosionDusts()
    {
        // Smoke Dust spawn
        for (int i = 0; i < 50; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
            Main.dust[dustIndex].velocity *= 1.4f;
        }
        // Fire Dust spawn
        for (int i = 0; i < 80; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 3f);
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].velocity *= 5f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 2f);
            Main.dust[dustIndex].velocity *= 3f;
        }
        // Large Smoke Gore spawn
        for (int g = 0; g < 2; g++)
        {
            int goreIndex = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            goreIndex = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[goreIndex].scale = 1.25f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            goreIndex = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[goreIndex].scale = 1.75f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            goreIndex = Gore.NewGore(Projectile.GetSource_FromThis(), new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
        }
    }
}