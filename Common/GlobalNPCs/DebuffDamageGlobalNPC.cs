using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalNPCs;

internal class DebuffDamageGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public enum SourceId
    {
        InfectedClip,
        MalignantForce,
        Hellfire,
        Other
    }
    public void AddBuffDamage(SourceId id, int buffType, int dps)
    {
        if (debuffDps.ContainsKey(id))
            return;
        else
        {
            debuffDps.Add(id, (buffType, dps));
        }
    }
    Dictionary<SourceId, (int buff, int dps)> debuffDps = new();
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        Dictionary<SourceId, (int, int)> newDict = new();
        foreach (var pair in debuffDps)
        {
            int buff = pair.Value.Item1;
            if (npc.HasBuff(buff))
            {
                int dps = pair.Value.Item2;
                npc.lifeRegen -= dps * 2;
                newDict.Add(pair.Key, pair.Value);
            }
        }
        debuffDps = newDict;
    }
}
