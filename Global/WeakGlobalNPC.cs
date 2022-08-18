namespace WarframeMod.Global;

internal class WeakGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    int weakPower = 0;
    int weakTime = 0;
    float DamageMultiplier => 1f / (MathF.Sqrt(weakPower) + 1f);
    public override void AI(NPC npc)
    {
        if (npc.HasBuff(BuffID.Weak))
        {
            weakPower++;
            int buffIndex = Array.IndexOf(npc.buffType, BuffID.Weak);
            int buffTime = npc.buffTime[buffIndex];
            if (weakTime < buffTime)
                weakTime = buffTime;
            npc.DelBuff(buffIndex);
        }
        if (weakTime <= 0)
        {
            weakPower = 0;
        }
        else
        {
            weakTime--;
        }
    }
    public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
    {
        damage = (int)(damage * DamageMultiplier);
    }
    public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        damage = (int)(damage * DamageMultiplier);
    }
}
