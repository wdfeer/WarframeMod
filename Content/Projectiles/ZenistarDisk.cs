using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

internal class ZenistarDisk : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MagnetSphereBall;
    public override void SetDefaults()
    {
        Projectile.hide = true;
        Projectile.timeLeft = Zenistar.DISK_DURATION;
        Projectile.tileCollide = false;
    }
    public override void PostAI()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector2 pos = Projectile.position + Main.rand.NextVector2Circular(16, 16);
            Dust.NewDustPerfect(pos, DustID.Shadowflame);
        }
    }
    public override bool PreAI()
    {
        Projectile.velocity *= 0.95f;
        return Projectile.velocity.Length() < 1.5f;
    }
    public float ShootRotation
    {
        get => Projectile.ai[0];
        set
        {
            Projectile.ai[0] = value;
        }
    }
    float ShootInterval => 15;
    float shootTimer = 0;
    public override void AI()
    {
        shootTimer++;
        if (shootTimer <= ShootInterval)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 velocity = (ShootRotation + MathF.PI / 1.5f * i).ToRotationVector2() * 16; 
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.position,
                    velocity,
                    ModContent.ProjectileType<ZenistarFlame>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    Projectile.owner);
            }

            ShootRotation += MathF.PI / 45;
            shootTimer = 0;
        }
    }
}
class ZenistarFlame : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;
    public override void SetDefaults()
    {
        Projectile.hide = true;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.penetrate = 2;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 15;
        Projectile.timeLeft = 15;
        Projectile.extraUpdates = 2;
        Projectile.netUpdate = true;
    }
    public override void AI()
    {
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Scale: 0.75f);
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        target.AddBuff(BuffID.ShadowFlame, 120);
    }
    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
        target.AddBuff(BuffID.ShadowFlame, 120);
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        target.AddBuff(BuffID.ShadowFlame, 120);
    }
}