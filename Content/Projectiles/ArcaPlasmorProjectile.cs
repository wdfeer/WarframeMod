using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;
internal class ArcaPlasmorProjectile : ModProjectile
{
    public bool tenet = false;
    public override void SetDefaults()
    {
        Projectile.width = 80;
        Projectile.height = 80;
        Projectile.scale = 0.6f;
        Projectile.alpha = 128;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 32;
        Projectile.light = 0.5f;
        Projectile.tileCollide = false;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }
    public override void AI()
    {
        Projectile.rotation = Convert.ToSingle(-Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y));
        for (int i = 0; i < (tenet ? 2 : 1); i++)
        {
            var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226)];
            dust.scale = 0.75f;
            Projectile.alpha = 256 - Projectile.timeLeft * (tenet ? 3 : 4);
            Projectile.light = Projectile.timeLeft * 0.01f;
        }
        if (!Projectile.tileCollide && TileColliding())
        {
            Projectile.tileCollide = true;
            for (int i = 0; i < 16; i++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 180, 0f, 0f, 75, default, 0.6f);
            }
        }
    }
    bool TileColliding()
    {
        float tileCollisionHitboxSizeMult = 0.1f;
        Vector2 pos = new Vector2(Projectile.Center.X - Projectile.width * tileCollisionHitboxSizeMult, Projectile.Center.Y - Projectile.height * tileCollisionHitboxSizeMult);
        int width = (int)(Projectile.width * tileCollisionHitboxSizeMult);
        int height = (int)(Projectile.height * tileCollisionHitboxSizeMult);
        bool colliding = Collision.SolidCollision(pos, width, height);
        return colliding;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.timeLeft > 16)
        {
            Projectile.timeLeft = 16;
            Projectile.velocity *= 0.5f;
        }
        return false;
    }
    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (Projectile.timeLeft > 12)
        {
            if (tenet) Projectile.timeLeft -= 8;
            else
            {
                Projectile.timeLeft = 12;
                Projectile.velocity *= 0.6f;
            }
        }
        for (int i = 0; i < 24; i++)
        {
            Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 180, 0f, 0f, 75, default, 0.6f);
        }
    }
}