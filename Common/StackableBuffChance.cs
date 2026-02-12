namespace WarframeMod.Common;
public enum StackableBuff
{
    Bleeding,
    Electricity,
    Toxin
}
public struct StackableBuffChance
{
    public StackableBuff type;
    public float chance;
    public StackableBuffChance(StackableBuff type, float chance)
    {
        this.type = type;
        this.chance = chance;
    }
    public StackableBuffChance(StackableBuff type, int chancePercent)
    {
        this.type = type;
        chance = chancePercent / 100f;
    }
    public void Apply(NPC npc, int damage)
    {
        DotBuff.Create(type, damage, npc);
    }
    public void RollAndApply(NPC npc, int damage)
    {
        if (Main.rand.NextFloat() > chance)
            return;
        Apply(npc, damage);
    }
    public static void ApplyBuffs(NPC target, IEnumerable<StackableBuffChance> buffChances, int damagePostCrit)
    {
        foreach (StackableBuffChance chance in buffChances)
        {
            chance.RollAndApply(target, damagePostCrit);
        }
    }
    public static void AddDebuffNoSync(int targetId, StackableBuff type, float damage)
    {
        NPC target = Main.npc[targetId];
        DotBuff.Create(type, (int)damage, target, false);
    }
}
