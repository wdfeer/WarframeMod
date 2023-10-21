using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Projectiles;

internal class ArumSpinosaProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DiamondBolt;
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.timeLeft = 75;
        Projectile.hide = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.height = 8;
        Projectile.width = 8;
        Projectile.penetrate = 3;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;

        var buffProj = Projectile.GetGlobalProjectile<BuffGlobalProjectile>();
        buffProj.stackableBuffChances.Add(new Common.StackableBuffChance(Common.StackableBuff.Bleed, 0.4f));
    }
    public override void AI()
    {
        for (int i = 0; i < 3; i++)
        {
            var dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 178)];
            dust.noGravity = true;
            dust.scale = 0.75f;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Venom, 600);
    }
}