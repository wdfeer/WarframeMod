using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Consumables;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class GrimoireAltProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;

    private const int baseTimeLeft = 180;
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.hide = true;
        Projectile.height = 90;
        Projectile.width = 90;
        Projectile.timeLeft = baseTimeLeft;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 20;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(100);
    }
    public override void AI()
    {
        float dustMultiplier = (float)Projectile.timeLeft / baseTimeLeft;
        DustHelper.NewDustsCircleEdge((int)(9 * dustMultiplier),
            Projectile.Center,
            Projectile.width / 2f,
            DustID.GemAmethyst,
            dust => dust.noGravity = true);
        DustHelper.NewDustsCircleFromCenter((int)(3 * dustMultiplier),
            Projectile.Center,
            Projectile.width / 3f,
            DustID.Electric,
            2f,
            d => d.noGravity = true);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        var owner = Main.player[Projectile.owner];
        if (Grimoire.GetPlayerGrimoire(owner).HasUpgrade(GrimoireUpgradeType.VomeInvocation))
        {
            owner.AddBuff(ModContent.BuffType<VomeInvocationBuff>(), 60 * 15);
            owner.GetModPlayer<VomeInvocationPlayer>().stacks++;
        }
    }
}