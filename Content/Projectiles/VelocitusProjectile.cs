using Terraria.Audio;

namespace WarframeMod;
public class VelocitusProjectile : ModProjectile
{
    const float MOVE_DISTANCE = 128;
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SniperBullet;
    public override void SetDefaults()
    {
        Projectile.tileCollide = false;
        Projectile.scale = 0;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 120;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.friendly = false;
    }
    public bool charged = false;
    Vector2 HitboxLineEnd => Projectile.position + new Vector2(0, 1).RotatedBy(Projectile.rotation) * 128;

    public override void AI()
    {
        if (Projectile.friendly)
        {
            return;
        }
        Player player = Main.player[Projectile.owner];
        if (!player.channel)
        {
            Launch();
        }
        else
        {
            if (charged)
            {
                Projectile.timeLeft = 60;
                if (Main.rand.NextBool(2))
                    Dust.NewDustPerfect(
                        player.Center + Vector2.Normalize(Projectile.velocity) * MOVE_DISTANCE * 0.45f,
                        DustID.Electric,
                        Scale: 0.5f);
            }
            else if (Projectile.timeLeft <= 60)
            {
                charged = true;
                SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
            }

            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.velocity = diff;
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }
            int dir = Projectile.direction;
            player.ChangeDir(dir); // Set player direction to where we are shooting
            player.heldProj = Projectile.whoAmI; // Update player's held Projectile
            player.itemTime = 8; // Set item time to 2 frames while we are used
            player.itemAnimation = 8; // Set item animation time to 2 frames while we are used
            player.itemRotation = MathF.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir); // Set the item rotation to where we are shooting
            Projectile.rotation = player.itemRotation + MathHelper.PiOver2 + MathHelper.Pi;
            Projectile.position = player.Center + Vector2.Normalize(Projectile.velocity) * MOVE_DISTANCE;
            if (Projectile.velocity.X > 0)
                Projectile.position -= Projectile.velocity * MOVE_DISTANCE;
        }
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        => Collision.CheckAABBvLineCollision(targetHitbox.Location.ToVector2(),
            targetHitbox.Size(),
            Projectile.position,
            HitboxLineEnd);
    void Launch()
    {
        Projectile.scale = 1;
        Projectile.friendly = true;
        Projectile.extraUpdates = 3;
        Projectile.netUpdate = true;
        Projectile.velocity *= 16;
        if (!charged)
        {
            float chargeMult = (121f - Projectile.timeLeft) / 60f;
            Projectile.damage = (int)(Projectile.damage * chargeMult);
            Projectile.knockBack *= chargeMult;
            Projectile.CritChance = (int)(Projectile.CritChance * (0.5f + chargeMult / 2));
        }
        Projectile.timeLeft = 120;
        Projectile.tileCollide = true;
        SoundEngine.PlaySound(SoundID.Item38, Projectile.Center);
    }
}