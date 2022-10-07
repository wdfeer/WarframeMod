using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Common;
public struct ElectricityBuff
{
    public float dps;
    public int timeLeft;
    public ElectricityBuff(float dps, int time)
    {
        this.dps = dps;
        timeLeft = time;
    }
    public static void Create(float hitDamage, NPC target)
    {
        target.GetGlobalNPC<StackableDebuffNPC>().electricity.Add(new ElectricityBuff(hitDamage / 5f, 300));
    }
    public static List<ElectricityBuff> UpdateAll(IEnumerable<ElectricityBuff> debuffs, out int damage)
    {
        List<ElectricityBuff> newDebuffs = new List<ElectricityBuff>();
        float totalDamage = 0;
        foreach (var item in debuffs)
        {
            totalDamage += item.dps;
            if (item.timeLeft > 0)
            {
                ElectricityBuff newDebuff = item;
                newDebuff.timeLeft--;
                newDebuffs.Add(newDebuff);
            }
        }
        damage = (int)totalDamage;
        return newDebuffs;
    }
    public static void Damage(NPC npc, int totalDmg)
    {
        npc.StrikeNPC(totalDmg, 0, -2);
    }
}
