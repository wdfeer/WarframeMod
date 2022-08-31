namespace WarframeMod.Common.GlobalNPCs;

internal class WeakGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    List<int> weakTimes = new List<int>();
    int WeakPower => weakTimes.Count;
    float DamageMultiplier => 1f / (MathF.Sqrt(WeakPower) + 1f);
    public override void AI(NPC npc)
    {
        if (npc.HasBuff(BuffID.Weak))
        {
            int buffIndex = Array.IndexOf(npc.buffType, BuffID.Weak);
            int buffTime = npc.buffTime[buffIndex];
            weakTimes.Add(buffTime);
            npc.DelBuff(buffIndex);
        }
        for (int i = 0; i < weakTimes.Count; i++)
        {
            weakTimes[i]--;
        }
        weakTimes = weakTimes.Where(x => x > 0).ToList();
    }
    public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
    {
        //int oldDamage = damage;
        if (npc.boss)
            damage = (int)(damage / 2 + damage * DamageMultiplier / 2);
        else
            damage = (int)(damage * DamageMultiplier);
        //Main.NewText($"OldDamage: {oldDamage}, NewDamage: {damage}. {WeakPower} weak stacks");
    }
    public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        damage = (int)(damage * DamageMultiplier);
    }
}
