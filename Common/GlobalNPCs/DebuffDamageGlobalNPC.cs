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
        if (extraDebuffDps.ContainsKey(id))
            return;
        else
        {
            extraDebuffDps.Add(id, (buffType, dps));
        }
    }
    Dictionary<SourceId, (int, int)> extraDebuffDps = new();
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        Dictionary<SourceId, (int, int)> newDict = new();
        foreach (var pair in extraDebuffDps)
        {
            int buff = pair.Value.Item1;
            if (npc.HasBuff(buff))
            {
                int dps = pair.Value.Item2;
                npc.lifeRegen -= dps * 2;
                newDict.Add(pair.Key, pair.Value);
            }
        }
        extraDebuffDps = newDict;
    }
}
