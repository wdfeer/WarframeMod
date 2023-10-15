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
    public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
    {
        if (npc.boss)
            modifiers.SourceDamage *= 0.5f + (DamageMultiplier * 0.5f);
        else
            modifiers.SourceDamage *= DamageMultiplier;
    }
    public override void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.SourceDamage *= DamageMultiplier;
    }
}
