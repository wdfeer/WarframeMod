using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class SimulorProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SapphireBolt;
    public const int baseTimeLeft = 660;
    public bool implosion = false;
    private float damageMult = 1f;
    public float DamageMult
    {
        get => damageMult;
        set
        {
            if (value > Simulor.MERGE_DAMAGE_INCREASE_MAX_PERCENT / 100f)
                value = Simulor.MERGE_DAMAGE_INCREASE_MAX_PERCENT / 100f;
            damageMult = value;
        }
    }
    const float mergeDamageIncrease = Simulor.MERGE_DAMAGE_INCREASE_PERCENT / 100f;

    int explosionWidth = 200;
    public override int ExplosionWidth => explosionWidth;

    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.height = 20;
        Projectile.width = 20;
        Projectile.timeLeft = baseTimeLeft;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.light = 0.7f;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Collision.SolidTilesVersatile((int)Projectile.Left.X, (int)Projectile.Left.Y, (int)Projectile.Right.X, (int)Projectile.Right.Y))
            Projectile.velocity.X = -oldVelocity.X * 0.5f;
        if (Collision.SolidTilesVersatile((int)Projectile.Bottom.X, (int)Projectile.Bottom.Y, (int)Projectile.Top.X, (int)Projectile.Top.Y))
            Projectile.velocity.Y = -oldVelocity.Y * 0.5f;
        return false;
    }
    public override void AI()
    {
        if (Projectile.timeLeft <= 8) TryExplode();
        if (exploding)
            return;
        if (Projectile.velocity.Length() > 0.1f) Projectile.velocity -= Vector2.Normalize(Projectile.velocity) * 0.2f;

        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile proj = Main.projectile[i];
            if (!proj.active || proj.ModProjectile is not SimulorProjectile) continue;
            SimulorProjectile simulorProj = proj.ModProjectile as SimulorProjectile;
            if (simulorProj == this || simulorProj.exploding) continue;
            float dist = (proj.position - Projectile.position).Length();
            if (dist > 480f) continue;
            if (dist > Projectile.width / 2)
            {
                Projectile.velocity -= Vector2.Normalize(Projectile.position - proj.position) * 100 / dist;
            }
            else
            {
                if (proj.timeLeft > Projectile.timeLeft)
                {
                    simulorProj.DamageMult += mergeDamageIncrease + simulorProj.DamageMult / 2;
                    DamageMult /= 2;
                    proj.timeLeft = baseTimeLeft;
                    proj.velocity *= 0.1f;
                    TryExplode(480, 100);
                }
                else
                {
                    DamageMult += mergeDamageIncrease + simulorProj.DamageMult / 2;
                    simulorProj.DamageMult /= 2;
                    Projectile.timeLeft = baseTimeLeft;
                    Projectile.velocity *= 0.1f;
                    simulorProj.TryExplode(480, 100);
                }
            }
        }

        DustHelper.NewDustsCircleEdge(3, Projectile.Center, Projectile.width / 2, 206, (dust) =>
            {
                dust.velocity *= 0.5f;
                dust.scale = 1.2f;
                dust.noGravity = true;
            });
    }
    /// <summary>
    /// Makes this Projectile explode with the chosen radius (width and height) and chance to proc Electrified
    /// </summary>
    /// <returns>False if the Projectile is already exploding, otherwise true</returns>
    public bool TryExplode(int radius = 200, int electricityChance = 30)
    {
        if (exploding) return false;
        explosionWidth = radius;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(electricityChance);
        if (electricityChance != 30)
        {
            Projectile.knockBack = 0f;
            implosion = true;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC target = Main.npc[i];
                if (target.boss || target.type == NPCID.TargetDummy || (target.Center - Projectile.Center).Length() > Projectile.width) continue;
                target.velocity += Vector2.Normalize(Projectile.Center - target.Center) * 6f;
            }
        }
        Explode();

        return true;
    }

    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCustom(ExplosionWidth / 12, () =>
            Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) * Projectile.width / (implosion ? 2 : 3), 226),
            (dust) =>
            {
                dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * (ExplosionWidth / 80f);
                if (implosion) dust.velocity *= -1.2f;
            });
    }

    public override bool? CanHitNPC(NPC target)
    {
        if (!exploding) return false;
        return base.CanHitNPC(target);
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.FinalDamage *= DamageMult;
    }
}
