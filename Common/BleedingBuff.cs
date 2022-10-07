using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Common;
public struct BleedingBuff
{
    public float dps;
    public int timeLeft;
    public BleedingBuff(float dps, int time)
    {
        this.dps = dps;
        timeLeft = time;
    }
    public static void Create(float hitDamage, NPC target)
    {
        target.GetGlobalNPC<StackableDebuffNPC>().bleeds.Add(new BleedingBuff(hitDamage / 5f, 300));
    }
    public static List<BleedingBuff> UpdateAll(IEnumerable<BleedingBuff> bleeds, out int damage)
    {
        List<BleedingBuff> newBleeds = new List<BleedingBuff>();
        float totalDamage = 0;
        foreach (var item in bleeds)
        {
            totalDamage += item.dps;
            if (item.timeLeft > 0)
            {
                BleedingBuff newBleed = item;
                newBleed.timeLeft--;
                newBleeds.Add(newBleed);
            }
        }
        damage = (int)totalDamage;
        return newBleeds;
    }
    public static void Damage(NPC npc, int bleed)
    {
        int oldDefense = npc.defense;
        npc.defense = 0;
        npc.StrikeNPC(bleed, 0, -2);
        npc.defense = oldDefense;
    }
}
