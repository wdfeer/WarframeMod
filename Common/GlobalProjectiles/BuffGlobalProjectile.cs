using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Common.GlobalProjectiles;

internal class BuffGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public float bleedingChance = 0;
    public float electricityChance = 0;
    public List<BuffChance> buffChances = new List<BuffChance>();
    public void Add(BuffChance bc)
    {
        buffChances.Add(bc);
    }
    public void HitNPCAfterCritModifiersApplied(NPC target, int damageAfterCrit)
    {
        BuffChance.ApplyBuffs(target, buffChances);
        var debuffer = target.GetGlobalNPC<StackableDebuffNPC>();
        if (Main.rand.NextFloat() < bleedingChance)
        {
            BleedingBuff.Create(damageAfterCrit, target);
        }
        if (Main.rand.NextFloat() < electricityChance)
        {
            ElectricityBuff.Create(damageAfterCrit, target);
        }
    }
}
