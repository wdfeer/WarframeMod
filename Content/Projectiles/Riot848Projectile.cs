using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class Riot848Projectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MoonlordBullet;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.MoonlordBullet);
        AIType = ProjectileID.MoonlordBullet;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBuff(StackableBuff.Weak, Riot848.WEAK_CHANCE);
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.penetrate = 1;
    }

    public override void OnKill(int timeLeft)
    {
        var proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(),
            Projectile.position,
            Vector2.Zero,
            ModContent.ProjectileType<Riot848ImpaledProjectile>(),
            Projectile.damage,
            Projectile.knockBack,
            Projectile.owner);
        proj.rotation = Projectile.rotation;
    }
}

public class Riot848ImpaledProjectile : ExplosiveProjectile
{
    public override int ExplosionWidth => 5 * 16;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.timeLeft = 20 * 60;
        Projectile.friendly = false;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = Vector2.Zero;
        return false;
    }

    public override void AI()
    {
        base.AI();

        if (Main.rand.NextFloat() < 0.2f)
            DustHelper.NewDustsCircleFromCenter(1, Projectile.Center, 4f, DustID.Vortex, 0.2f, d => d.scale = 0.5f);

        Projectile.velocity.Y = MathF.Min(8f, Projectile.velocity.Y + 0.1f);
    }

    public override void Explode()
    {
        Projectile.friendly = true;
        base.Explode();
    }

    public override void OnKill(int timeLeft)
    {
        base.OnKill(timeLeft);
        Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(),
            Projectile.position,
            // TODO: not very accurate, aims too high above the player
            16f * Projectile.Center.DirectionTo(Main.player[Projectile.owner].Center),
            ModContent.ProjectileType<Riot848ReturningProjectile>(),
            Projectile.damage,
            Projectile.knockBack,
            Projectile.owner);
    }

    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCircleFromCenter(8, Projectile.Center, ExplosionWidth / 4f, DustID.Electric, 0.4f);
        DustHelper.NewDustsCircleFromCenter(2, Projectile.Center, ExplosionWidth / 2f, DustID.Vortex, 0.2f,
            dust => dust.noGravity = true);
    }
}

public class Riot848ReturningProjectile : Riot848Projectile
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.penetrate = -1;
        Projectile.extraUpdates = 3;
    }

    public override void OnKill(int timeLeft)
    {
    } // do not create an impaled projectile

    public override void AI()
    {
        // TODO: make it delete itself more consistently, e.g. with homing
        var ownerPos = Main.player[Projectile.owner].Center;
        if (Projectile.Distance(ownerPos) < 32f)
            Projectile.Kill();
    }
}