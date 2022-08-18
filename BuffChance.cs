namespace WarframeMod;

public struct BuffChance
{
    int type;
    int time;
    float chance;
    public BuffChance(int type, int time, float chance)
    {
        this.type = type;
        this.time = time;
        this.chance = chance;
    }
    public void Apply(NPC npc)
    {
        npc.AddBuff(type, time);
    }
    public void RollAndApply(NPC npc)
    {
        if (Main.rand.NextFloat() > chance)
            return;
        Apply(npc);
    }
    public static void ApplyBuffs(NPC target, IEnumerable<BuffChance> buffChances)
    {
        foreach (BuffChance chance in buffChances)
        {
            chance.RollAndApply(target);
        }
    }
}
