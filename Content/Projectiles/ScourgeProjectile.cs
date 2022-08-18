namespace WarframeMod.Content.Projectiles;

/// <summary>
/// This the class that clones the vanilla Meowmere projectile using CloneDefaults().
/// Make sure to check out <see cref="ExampleCloneWeapon" />, which fires this projectile; it itself is a cloned version of the Meowmere.
/// </summary>
public class ScourgeProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.EmeraldBolt;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Scourge");
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.EmeraldBolt);
        AIType = ProjectileID.EmeraldBolt;
        Projectile.penetrate = 1;
    }
    const int numOfProjectilesSpawnedOnKill = 5;
    public override void Kill(int timeLeft)
    {
        Vector2 launchVelocity = new Vector2(-4, 0); // Create a velocity moving the left.
        launchVelocity = launchVelocity.RotatedByRandom(MathHelper.Pi);
        for (int i = 0; i < numOfProjectilesSpawnedOnKill; i++)
        {
            // Every iteration, rotate the newly spawned projectile by the equivalent 1/4th of a circle (MathHelper.PiOver4)
            // (Remember that all rotation in Terraria is based on Radians, NOT Degrees!)
            launchVelocity = launchVelocity.RotatedBy(MathHelper.Pi * 2 / numOfProjectilesSpawnedOnKill);

            // Spawn a new projectile with the newly rotated velocity, belonging to the original projectile owner. The new projectile will inherit the spawning source of this projectile.
            int projectileID = Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, launchVelocity, ProjectileID.EmeraldBolt, Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
            Projectile projectile = Main.projectile[projectileID];
            projectile.timeLeft = 45;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }
    }
}
