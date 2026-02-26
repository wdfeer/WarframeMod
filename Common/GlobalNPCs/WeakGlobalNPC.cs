namespace WarframeMod.Common.GlobalNPCs;

internal class WeakGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    
    List<int> weakTimes = [];
    int WeakPower => weakTimes.Count;

    public static void ApplyWeak(NPC npc)
    {
        npc.AddBuff(BuffID.Weak, 300);
        npc.GetGlobalNPC<WeakGlobalNPC>().weakTimes.Add(300);
    }

    public override void AI(NPC npc)
    {
        for (int i = 0; i < weakTimes.Count; i++)
        {
            weakTimes[i]--;
        }
        weakTimes = weakTimes.Where(x => x > 0).ToList();
    }

    float DamageDealtMult => WeakPower > 0 ? 0.75f / (MathF.Sqrt(WeakPower) + 1f) : 1f;
    private float BossDamageMult => 0.6f + DamageDealtMult * 0.4f;

    public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
    {
        modifiers.SourceDamage *= npc.boss ? BossDamageMult : DamageDealtMult;
    }

    public override void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.SourceDamage *= DamageDealtMult;
    }
}