namespace WarframeMod.Common.Players;

public class BuffPlayer : CritPlayerHooks
{
    public List<BuffChance> buffsOnHitNPC;

    public Dictionary<DamageClass, List<StackableBuffChance>> stackableBuffsOnHitNPC
        = new() { { DamageClass.Generic, new() } };

    public void AddBuffChance(StackableBuff type, int chancePercent, DamageClass damageClass = null)
    {
        if (damageClass == null)
            damageClass = DamageClass.Generic;

        var chance = new StackableBuffChance(type, chancePercent);

        if (stackableBuffsOnHitNPC.ContainsKey(damageClass))
            stackableBuffsOnHitNPC[damageClass].Add(chance);
        else
            stackableBuffsOnHitNPC[damageClass] = new List<StackableBuffChance>() { chance };
    }

    [Obsolete]
    public void AddBleedChance(DamageClass damageType, float chance)
    {
        AddBuffChance(StackableBuff.Bleeding, (int)(chance * 100f), damageType);
    }

    public override void ResetEffects()
    {
        buffsOnHitNPC = [];
        stackableBuffsOnHitNPC = new()
            { { DamageClass.Generic, [] } };
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        => BuffChance.ApplyBuffs(target, buffsOnHitNPC);

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        => BuffChance.ApplyBuffs(target, buffsOnHitNPC);

    public override void OnHitNPCPostCrit(Item item, NPC target, int damage, float knockback, bool crit, float critMult,
        int critLvl, int damagePostCrit)
    {
        foreach (var pair in stackableBuffsOnHitNPC)
            if (item.DamageType.CountsAsClass(pair.Key)
                // Shouldn't be necessary but is for some reason
                || pair.Key == DamageClass.Generic)
                StackableBuffChance.ApplyBuffs(target, pair.Value, damagePostCrit);
    }

    public override void OnHitNPCWithProjPostCrit(Projectile proj, NPC target, int damage, float knockback, bool crit,
        float critMult, int critLvl, int damagePostCrit)
    {
        foreach (var pair in stackableBuffsOnHitNPC)
            if (proj.DamageType.CountsAsClass(pair.Key)
                // Shouldn't be necessary but is for some reason
                || pair.Key == DamageClass.Generic)
                StackableBuffChance.ApplyBuffs(target, pair.Value, damagePostCrit);
    }
}