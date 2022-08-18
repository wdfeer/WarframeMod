using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Common.GlobalProjectiles;

internal class BuffGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    /// <summary>
    /// Bleed chance from 0 to 1
    /// </summary>
    public float bleedingChance = 0;
    public List<BuffChance> buffChances = new List<BuffChance>();
    public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        BuffChance.ApplyBuffs(target, buffChances);
        if (Main.rand.NextFloat() < bleedingChance)
        {
            var bleedNPC = target.GetGlobalNPC<BleedingGlobalNPC>();
            bleedNPC.bleeds.Add(new BleedingBuff(damage / 5 * (crit ? 2 : 1), 300));
        }
    }
}
