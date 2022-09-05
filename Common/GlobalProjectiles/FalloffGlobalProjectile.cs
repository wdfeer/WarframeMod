namespace WarframeMod.Common.GlobalProjectiles;

internal class FalloffGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    Vector2 initialPosition;
    float minFalloffDistance;
    float maxFalloffDistance;
    float maxFalloffDamageDecrease = 0f;
    public void SetFalloff(Vector2 initialPosition, float minDistance, float maxDistance, float maxDamageDecrease)
    {
        this.initialPosition = initialPosition;
        minFalloffDistance = minDistance;
        maxFalloffDistance = maxDistance;
        maxFalloffDamageDecrease = maxDamageDecrease;
    }
    public void SetFalloff(Vector2 initialPosition, int minTiles, int maxTiles, float maxDamageDecrease)
    {
        SetFalloff(initialPosition, minTiles * 16f, maxTiles * 16f, maxDamageDecrease);
    }
    float GetDamageMult(Projectile proj)
    {
        float travelled = proj.position.Distance(initialPosition);
        if (travelled < minFalloffDistance)
            return 1f;
        else if (travelled > maxFalloffDistance)
            return 1f - maxFalloffDamageDecrease;

        float travelledToMaxRatio = (travelled - minFalloffDistance) / (maxFalloffDistance - minFalloffDistance);
        float decrease = travelledToMaxRatio * maxFalloffDamageDecrease;
        return 1f - decrease;
    }
    public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (maxFalloffDamageDecrease > 0f)
            damage = (int)(damage * GetDamageMult(projectile));
    }
}
