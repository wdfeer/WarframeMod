using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Common.Players;
public class BuffPlayer : ModPlayer
{
    public List<BuffChance> onHitNPC;
    Dictionary<int, float> bleedingChances = new Dictionary<int, float>();
    int GetDamageClassID(DamageClass damageClass)
    {
        if (damageClass == DamageClass.SummonMeleeSpeed)
            return DamageClass.Summon.Type;
        if (damageClass == DamageClass.MeleeNoSpeed)
            return DamageClass.Melee.Type;
        return damageClass.Type;
    }
    public void AddBleedChance(DamageClass damageType, float chance)
    {
        int type = GetDamageClassID(damageType);
        if (bleedingChances.ContainsKey(type))
        {
            bleedingChances[type] += chance;
        }
        else
        {
            bleedingChances.Add(type, chance);
        }
    }
    public void ApplyBleedChances(NPC target, int damage, DamageClass damageType)
    {
        int type = GetDamageClassID(damageType);
        if (!bleedingChances.ContainsKey(type))
            return;
        if (Main.rand.NextFloat() < bleedingChances[type])
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
