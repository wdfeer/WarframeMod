namespace WarframeMod.Common.GlobalProjectiles;

internal class CritGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public float CritMultiplier { get; set; } = 1f;
    public override void PostAI(Projectile projectile)
    {
        if (Main.projPet[projectile.type] && projectile.damage > 0)
        {
            Player owner = Main.player[projectile.owner];
            Item spawnedFrom = owner.inventory.First(i => i.shoot == projectile.type);
            int crit = 4;
            if (spawnedFrom != null)
                crit = owner.GetWeaponCrit(spawnedFrom);
            else
                crit = (int)owner.GetTotalCritChance(projectile.DamageType);
            projectile.CritChance = crit;
        }
    }
}
