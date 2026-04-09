namespace WarframeMod.Common.GlobalNPCs.Eximus;

public abstract class EximusVariant : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public bool enabled;
    public override void ResetEffects(NPC npc)
    {
        enabled = true;
    }
}