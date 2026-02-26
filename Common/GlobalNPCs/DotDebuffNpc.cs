namespace WarframeMod.Common.GlobalNPCs;

// Call DotBuff.Create instead of using this directly
public class DotDebuffNpc : GlobalNPC
{
    public override bool InstancePerEntity => true;

    // Individual procs with their own damage and timers
    public List<DotBuff> dots = [];

    // Damage timers for each proc type
    public Dictionary<StackableBuff, int> timers = new();
    public int DotTypeCount => timers.Count;
    public bool HasDoT(StackableBuff type) => timers.ContainsKey(type);

    const int tickTime = 60;

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        Dictionary<StackableBuff, float> dps = new();

        foreach (DotBuff dot in dots.ToList())
        {
            timers.TryAdd(dot.type, 0);

            if (dps.ContainsKey(dot.type))
                dps[dot.type] += dot.dps;
            else
                dps[dot.type] = dot.dps;

            dot.timeLeft--;
            if (dot.timeLeft <= 0)
                dots.Remove(dot);
        }

        foreach (StackableBuff type in timers.Keys)
        {
            if (!dps.ContainsKey(type))
                timers.Remove(type);
            else
            {
                timers[type]--;
                if (timers[type] <= 0)
                {
                    timers[type] = tickTime;

                    int tickDamage = (int)(dps[type] * 60f / tickTime);
                    DamageTick(npc, type, tickDamage);
                    CreateDust(npc, type);
                }
            }
        }
    }

    private Dictionary<StackableBuff, Func<NPC, int, NPC.HitInfo>> hitInfoFactories = new()
    {
        {
            StackableBuff.Bleeding,
            (_, damage) => new NPC.HitInfo() { SourceDamage = damage, Damage = damage, HitDirection = -2 }
        },
        {
            StackableBuff.Electricity,
            (npc, damage) => new NPC.HitInfo()
                { Damage = (int)(damage - npc.defense * 0.75f), Knockback = 0 }
        },
        {
            StackableBuff.Toxin,
            (npc, damage) => new NPC.HitInfo()
                { Damage = (int)(damage - npc.defense * 0.75f), Knockback = 0 }
        }
    };

    private void DamageTick(NPC npc, StackableBuff type, int tickDamage)
    {
        NPC.HitInfo hitInfo = hitInfoFactories[type](npc, tickDamage);
        npc.StrikeNPC(hitInfo);
    }

    private static void CreateDust(NPC npc, StackableBuff type)
    {
        switch (type)
        {
            case StackableBuff.Electricity:
                DustHelper.NewDustsCircle((int)(MathF.Min(100f, npc.width / 16f + 8)),
                    npc.Center,
                    npc.width * 0.8f,
                    DustID.Electric,
                    d => d.noGravity = true);
                break;
            case StackableBuff.Toxin:
                DustHelper.NewDustsCircle((int)(MathF.Min(100f, npc.width / 16f + 8)),
                    npc.Center,
                    npc.width * 0.8f,
                    DustID.Toxikarp,
                    d => d.noGravity = true);
                break;
        }
    }
}