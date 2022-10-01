namespace WarframeMod.Common.GlobalItems;

public class CritGlobalItem : GlobalItem
{
    public override bool InstancePerEntity => true;
    public float critMultiplier = 1f;
}
