namespace WarframeMod.Common.GlobalItems;

public class BleedingGlobalItem : GlobalItem
{
    public override bool InstancePerEntity => true;
    public float bleedingChance = 0f; //used in CritPlayer
}
