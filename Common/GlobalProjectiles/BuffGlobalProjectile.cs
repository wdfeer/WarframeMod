namespace WarframeMod.Common.GlobalProjectiles;

internal class BuffGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public List<StackableBuffChance> stackableBuffChances = [];
    public void AddBleed(float chance) => stackableBuffChances.Add(new StackableBuffChance(StackableBuff.Bleeding, chance));
    public void AddBleed(int chance) => stackableBuffChances.Add(new StackableBuffChance(StackableBuff.Bleeding, chancePercent: chance));
    public void AddElectro(float chance) => stackableBuffChances.Add(new StackableBuffChance(StackableBuff.Electricity, chance));
    public void AddElectro(int chance) => stackableBuffChances.Add(new StackableBuffChance(StackableBuff.Electricity, chancePercent: chance));
    public List<BuffChance> buffChances = [];
    public void AddBuff(BuffChance bc)
    {
        buffChances.Add(bc);
    }
    public void HitNPCAfterCritModifiersApplied(NPC target, int damageAfterCrit)
    {
        BuffChance.ApplyBuffs(target, buffChances);
        StackableBuffChance.ApplyBuffs(target, stackableBuffChances, damageAfterCrit);
    }
}
