using WarframeMod.Common;

namespace WarframeMod.Content.Projectiles.Hostile;

public class ShockEximusProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SapphireBolt;

    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.hide = true;
        Projectile.tileCollide = false;
    }

    private const int HomingDistance = 16 * 64;
    private const float MaxSpeed = 10 * 16f / 60f;
    private const float Acceleration = MaxSpeed / 2f / 60f;

    public override void AI()
    {
        Player target = Main.player.Where(player => player.active && !player.dead)
            .MinBy(player => player.Distance(Projectile.position));

        if (target != null && target.Distance(Projectile.position) < HomingDistance)
        {
            var direction = Projectile.Center.DirectionTo(target.Center);
            Projectile.velocity += direction * Acceleration;
            if (Projectile.velocity.Length() > MaxSpeed)
            {
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero) * MaxSpeed;
            }

            // Don't decay if a player is nearby
            Projectile.timeLeft = 60;
        }
    }

    public override void PostAI()
    {
        DustHelper.NewDustsCircleEdge(1, Projectile.Center, Projectile.width / 2, DustID.Electric, 
            dust =>
            {
                dust.velocity *= 0.1f;
                dust.noGravity = true;
            });
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.Electrified, Main.expertMode ? 600 : 120);
    }
}