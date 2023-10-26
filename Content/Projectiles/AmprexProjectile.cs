using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;
internal class AmprexProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;
    public override void SetDefaults()
    {
        Projectile.width = 1;
        Projectile.height = 1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.friendly = true;
        Projectile.hide = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.extraUpdates = 100;
        Projectile.timeLeft = 48;
        Projectile.penetrate = 3;
    }
    const int HOMING_DISTANCE = 240;
    List<NPC> hitNPCs = new();
    public override bool PreAI()
    {
        NPC closestEnemy = Main.npc.Where(x => x != null
            && x.active
            && !x.friendly
            && x.CanBeChasedBy()
            && !hitNPCs.Contains(x)
            && Collision.CanHitLine(Projectile.position, 1, 1, x.Center, 1, 1))
            .MinBy(x => x.Center.Distance(Projectile.position));

        if (closestEnemy != null)
        {
            float distance = closestEnemy.Center.Distance(Projectile.position);
            if (distance <= HOMING_DISTANCE)
            {
                Vector2 direction = Projectile.position.DirectionTo(closestEnemy.Center);
                Projectile.velocity = 16 * direction;
            }
        }

        return true;
    }
    public override void AI()
    {
        float sineValue = MathF.Sin(Projectile.position.X / 30f);
        Projectile.position.Y += sineValue * 10f;
    }
    public override void PostAI()
    {
        float dustOffsetX = 24 * (Main.rand.NextFloat() - 0.5f);
        float dustOffsetY = 24 * (Main.rand.NextFloat() - 0.5f);
        for (int i = 0; i < 2; i++)
        {
            Vector2 pos = Projectile.Center + new Vector2(dustOffsetX, dustOffsetY);
            Dust dust = Dust.NewDustDirect(pos, 1, 1, DustID.Electric, Scale: 0.6f);
            dust.velocity *= 0.6f;
            dust.noGravity = true;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        hitNPCs.Add(target);
        Projectile.damage -= (int)(Projectile.damage * Amprex.MULTIHIT_DAMAGE_REDUCTION_PERCENT / 100f);
        if (Projectile.damage <= 1)
            Projectile.penetrate = 0;
    }
}