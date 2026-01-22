using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;
internal class LenzProjArrow : ExplosiveProjectile
{
    public override int ExplosionWidth => 320;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
        base.SetDefaults();
        Projectile.height = 30;
        Projectile.width = 30;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBuff(new Common.BuffChance(ModContent.BuffType<ColdDebuff>(), Lenz.COLD_DURATION, 1f));
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        canImpale = false;
        return base.OnTileCollide(oldVelocity);
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        base.ModifyHitNPC(target, ref modifiers);

        modifiers.SourceDamage /= 16;
        if (canImpale)
        {
            hitNpc = target;
            modifiers.SourceDamage *= 3;
            canImpale = false;
        }
    }
    public override void Explode()
    {
        base.Explode();
        canImpale = false;
    }
    bool canImpale = true;
    NPC hitNpc;
    public override void OnKill(int timeLeft)
    {
        var proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<LenzProjBubble>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = Projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier;
        if (hitNpc != null && hitNpc.active)
        {
            Vector2 offset = Projectile.Center - hitNpc.Center;

            (proj.ModProjectile as LenzProjBubble).Impale(hitNpc, offset);
        }
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < 45; i++)
        {
            int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceRod, 0f, 0f, 100, default(Color), 1.2f);
            var dust = Main.dust[dustIndex];
            dust.noGravity = true;
            dust.velocity *= 0.75f;
        }
    }
    public override void ExplosionSound() { }
}