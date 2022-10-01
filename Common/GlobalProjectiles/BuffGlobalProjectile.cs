using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Common.GlobalProjectiles;

internal class BuffGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    /// <summary>
    /// Bleed chance from 0 to 1
    /// </summary>
    public float bleedingChance = 0;
    public List<BuffChance> buffChances = new List<BuffChance>();
    public void Add(BuffChance bc)
    {
        buffChances.Add(bc);
    }
    public void HitNPCAfterCritModifiersApplied(NPC target, int damageAfterCrit)
    {
        BuffChance.ApplyBuffs(target, buffChances);
        if (Main.rand.NextFloat() < bleedingChance)
        {
            var bleedNPC = target.GetGlobalNPC<BleedingGlobalNPC>();
            bleedNPC.bleeds.Add(new BleedingBuff(damageAfterCrit / 5, 300));
        }
    }
}
