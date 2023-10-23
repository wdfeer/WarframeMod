namespace WarframeMod.Common;
public enum StackableBuff
{
    Bleed,
    Electro
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
        this.chance = chancePercent / 100f;
    }
    public void Apply(NPC npc, int damage)
    {
        switch (type)
        {
            case StackableBuff.Bleed:
                BleedingBuff.Create(damage, npc);
                break;
            case StackableBuff.Electro:
                ElectricityBuff.Create(damage, npc);
                break;
            default:
                break;
        }
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
        switch (type)
        {
            case StackableBuff.Bleed:
                BleedingBuff.Create(damage, target, false);
                break;
            case StackableBuff.Electro:
                ElectricityBuff.Create(damage, target, false);
                break;
            default:
                break;
        }
    }
}
