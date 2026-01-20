namespace WarframeMod.Common.GlobalProjectiles;

internal class BuffGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public List<StackableBuffChance> stackableBuffChances = [];
    public void AddBleed(float chance) => stackableBuffChances.Add(new StackableBuffChance(StackableBuff.Bleed, chance));
    public void AddBleed(int chance) => stackableBuffChances.Add(new StackableBuffChance(StackableBuff.Bleed, chancePercent: chance));
    public void AddElectro(float chance) => stackableBuffChances.Add(new StackableBuffChance(StackableBuff.Electro, chance));
    public void AddElectro(int chance) => stackableBuffChances.Add(new StackableBuffChance(StackableBuff.Electro, chancePercent: chance));
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
