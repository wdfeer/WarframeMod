namespace WarframeMod.Global;

internal class CritGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public float CritMultiplier { get; set; } = 1f;
    public int critLevel;
}
