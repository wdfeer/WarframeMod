namespace WarframeMod.Common.Players;
public class BuffPlayer : CritPlayerHooks
{
    public List<BuffChance> buffsOnHitNPC;
    public List<StackableBuffChance> stackableBuffsOnHitNPC;
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
            BleedingBuff.Create(damage, target);
    }
    public override void ResetEffects()
    {
        buffsOnHitNPC = [];
        stackableBuffsOnHitNPC = [];
        bleedingChances = new();
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        BuffChance.ApplyBuffs(target, buffsOnHitNPC);
    }
    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
    {
        BuffChance.ApplyBuffs(target, buffsOnHitNPC);
    }
    public override void OnHitNPCPostCrit(Item item, NPC target, int damage, float knockback, bool crit, float critMult, int critLvl, int damagePostCrit)
    {
        ApplyBleedChances(target, damagePostCrit, item.DamageType);
        StackableBuffChance.ApplyBuffs(target, stackableBuffsOnHitNPC, damagePostCrit);
    }
    public override void OnHitNPCWithProjPostCrit(Projectile proj, NPC target, int damage, float knockback, bool crit, float critMult, int critLvl, int damagePostCrit)
    {
        ApplyBleedChances(target, damagePostCrit, proj.DamageType);
        StackableBuffChance.ApplyBuffs(target, stackableBuffsOnHitNPC, damagePostCrit);
    }
}
