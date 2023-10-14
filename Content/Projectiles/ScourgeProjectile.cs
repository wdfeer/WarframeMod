using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;
public class ScourgeProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.EmeraldBolt;
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.EmeraldBolt);
        AIType = ProjectileID.EmeraldBolt;
        Projectile.penetrate = 1;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBuff(new Common.BuffChance(BuffID.Poisoned, 240, 1f));
        Projectile.ArmorPenetration = Scourge.DEFENSE_PENETRATION;
    }
    const int numOfProjectilesSpawnedOnKill = 5;
    public override void Kill(int timeLeft)
    {
        Vector2 launchVelocity = new Vector2(-4, 0);
        launchVelocity = launchVelocity.RotatedByRandom(MathHelper.Pi);
        for (int i = 0; i < numOfProjectilesSpawnedOnKill; i++)
        {
            launchVelocity = launchVelocity.RotatedBy(MathHelper.Pi * 2 / numOfProjectilesSpawnedOnKill);

            int projectileID = Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, launchVelocity, ProjectileID.EmeraldBolt, Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
            Projectile projectile = Main.projectile[projectileID];
            projectile.timeLeft = 45;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
            projectile.ArmorPenetration = Scourge.DEFENSE_PENETRATION;
        }
    }
}
