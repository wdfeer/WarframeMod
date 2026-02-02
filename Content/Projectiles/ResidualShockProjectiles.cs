using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Projectiles;

public class ResidualShockSpawner : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.None;

    public override void SetDefaults()
    {
        Projectile.hide = true;
        Projectile.friendly = false;
        Projectile.hostile = false;
        Projectile.timeLeft = ResidualShock.DURATION;
    }

    public override void AI()
    {
        DustHelper.NewDustsCircleEdge(1, Projectile.Center, 16f, DustID.Electric, dust =>
        {
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
        });
        if (Projectile.timeLeft % 10 == 0)
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmethyst);
            d.velocity *= 0;
            d.noGravity = true;
        }

        if (Projectile.timeLeft % 30 == 0)
        {
            NPC target = Main.npc.Where(it =>
                    !it.friendly && it.CanBeChasedBy() && it.Distance(Projectile.position) < ResidualShock.RANGE)
                .MinBy(it => it.Distance(Projectile.position));
            
            if (target != null)
            {
                Vector2 velocity = Vector2.Normalize(target.Center - Projectile.Center) * 16f;
                Projectile.NewProjectileDirect(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    velocity,
                    ModContent.ProjectileType<ResidualShockBolt>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    owner: Projectile.owner);
            }
        }
    }
}

public class ResidualShockBolt : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.None;

    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Summon;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.timeLeft = 60;
        Projectile.extraUpdates = 4;
        Projectile.tileCollide = false;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 10;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(100);
    }

    public override void AI()
    {
        if (Projectile.timeLeft % 2 == 0)
        {
            Dust.NewDustPerfect(Projectile.Center, DustID.Electric);
        }
    }
}