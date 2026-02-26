using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Common;

public static class CustomExtensions
{
    public static int GetDamageWithoutDefense(this NPC.HitModifiers modifiers, float baseDamage, bool crit)
    {
        var modifiersWithoutDefense = modifiers with { SuperArmor = false };
        modifiersWithoutDefense.Defense *= 0f;
        return modifiersWithoutDefense.GetDamage(baseDamage, crit);
    }

    public static int GetStatusCount(this NPC npc)
        => npc.buffTime.Count(it => it > 0) + npc.GetGlobalNPC<DotDebuffNpc>().DotTypeCount;
}