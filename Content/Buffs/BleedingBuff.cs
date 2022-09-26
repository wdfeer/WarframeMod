using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Buffs;
public struct BleedingBuff
{
    public int dps;
    public int timeLeft;
    public BleedingBuff(int dps, int time)
    {
        this.dps = dps;
        timeLeft = time;
    }
    public static void CreateBleed(int hitDamage, NPC target)
    {
        target.GetGlobalNPC<BleedingGlobalNPC>().bleeds.Add(new BleedingBuff(hitDamage / 5, 300));
    }
    public static List<BleedingBuff> UpdateBleeds(IEnumerable<BleedingBuff> bleeds, out int damage)
    {
        List<BleedingBuff> newBleeds = new List<BleedingBuff>();
        damage = 0;
        foreach (var item in bleeds)
        {
            damage += item.dps;
            if (item.timeLeft > 0)
            {
                BleedingBuff newBleed = item;
                newBleed.timeLeft--;
                newBleeds.Add(newBleed);
            }
        }
        return newBleeds;
    }
}
