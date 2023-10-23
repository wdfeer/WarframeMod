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
    public static void Create(float hitDamage, NPC target, bool netSync = true)
    {
        target.GetGlobalNPC<StackableDebuffNPC>().electricity.Add(new ElectricityBuff(hitDamage / 5f, 300));
        if (Main.netMode != NetmodeID.SinglePlayer && netSync)
            WarframeMod.instance.SendStackableDebuffPacket(target.whoAmI, StackableBuff.Electro, (int)hitDamage);
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
        // Setting the SourceDamage to totalDmg sets the actual Damage to 1 for some reason
        NPC.HitInfo hitInfo = new NPC.HitInfo() { Damage = (int)(totalDmg - npc.defense * 0.75f), Knockback = 0 };
        npc.StrikeNPC(hitInfo);
    }
}
