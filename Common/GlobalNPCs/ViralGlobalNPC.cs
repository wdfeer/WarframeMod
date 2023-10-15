using WarframeMod.Content.Buffs;

namespace WarframeMod.Common.GlobalNPCs;
internal class ViralGlobalNPC : GlobalNPC
{
    public const float VIRAL_DMG_MULT = 1.15f;
    public const int EXTRA_DAMAGE_PERCENT = 15;
    bool HasCold(NPC npc) => npc.HasBuff(ModContent.BuffType<ColdDebuff>()) || npc.HasBuff(BuffID.Frostburn);
    bool HasToxin(NPC npc) => npc.HasBuff(BuffID.Poisoned) || npc.HasBuff(BuffID.Venom);
    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        if (HasCold(npc) && HasToxin(npc))
            modifiers.SourceDamage *= VIRAL_DMG_MULT;
    }
}
