using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Common.Players;
public class BuffPlayer : ModPlayer
{
    public List<BuffChance> onHitNPC;
    Dictionary<int, float> bleedingChances = new Dictionary<int, float>();
    public void AddBleedChance(DamageClass damageType, float chance)
    {
        if (bleedingChances.ContainsKey(damageType.Type))
        {
            bleedingChances[damageType.Type] += chance;
        }
        else
        {
            bleedingChances.Add(damageType.Type, chance);
        }
    }
    public void ApplyBleedChances(NPC target, int damage, DamageClass damageType)
    {
        if (!bleedingChances.ContainsKey(damageType.Type))
            return;
        if (Main.rand.NextFloat() < bleedingChances[damageType.Type])
            BleedingBuff.CreateBleed(damage, target);
    }
    public override void ResetEffects()
    {
        onHitNPC = new List<BuffChance>();
        bleedingChances = new();
    }
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        BuffChance.ApplyBuffs(target, onHitNPC);
        ApplyBleedChances(target, damage, item.DamageType);
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        BuffChance.ApplyBuffs(target, onHitNPC);
        ApplyBleedChances(target, damage, proj.DamageType);
    }
}
