﻿namespace WarframeMod.Content.Projectiles;
public class OpticorProjectile : ExplosiveProjectile
{
    public override int ExplosionWidth => 160;
    public override bool ExplodeOnNPCHit => false;
    public virtual float MoveDistance => 80;
    public virtual int ChargeTime => 120;
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DiamondBolt;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.tileCollide = false;
        Projectile.scale = 0;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 60 + ChargeTime;
        Projectile.friendly = false;
    }
    public bool charged = false;
    public override bool PreAI()
    {
        if (Projectile.friendly)
            return false;
        Player player = Main.player[Projectile.owner];
        if (player.dead)
        {
            Projectile.Kill();
            return false;
        }
        return true;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (charged)
        {
            Launch();
        }
        else
        {
            if (!charged && Projectile.timeLeft <= 60)
            {
                charged = true;
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
            Projectile.position = player.Center + Vector2.Normalize(Projectile.velocity) * MoveDistance;
            if (Projectile.velocity.X > 0)
                Projectile.position -= Projectile.velocity * MoveDistance;
        }
    }
    public override void PostAI()
    {
        Player player = Main.player[Projectile.owner];
        if (!Projectile.friendly)
            Dust.NewDustPerfect(
                player.Center + Vector2.Normalize(Projectile.velocity) * MoveDistance * 0.7f,
                DustID.Electric,
                Scale: 0.7f);
        else if (Projectile.position.Distance(player.position) < 1200)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Scale: 1.2f);
        }
    }
    void Launch()
    {
        Projectile.position.Y -= 8;
        Projectile.friendly = true;
        Projectile.extraUpdates = 60;
        Projectile.netUpdate = true;
        Projectile.velocity *= 16;
        Projectile.timeLeft = 420;
        Projectile.tileCollide = true;
        Projectile.width = 20;
        Projectile.height = 20;
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 44; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            dust.velocity *= 0;
            dust.noGravity = true;
        }
    }
    public override void ExplosionSound() { }
}