using WarframeMod.Content.Buffs;

namespace WarframeMod.Common.GlobalNPCs;
internal class ViralGlobalNPC : GlobalNPC
{
    public const float VIRAL_DMG_MULT = 1.15f;
    public const int EXTRA_DAMAGE_PERCENT = 15;
    bool HasCold(NPC npc) => npc.HasBuff(ModContent.BuffType<ColdDebuff>()) || npc.HasBuff(BuffID.Frostburn);
    bool HasToxin(NPC npc) => npc.HasBuff(BuffID.Poisoned) || npc.HasBuff(BuffID.Venom);
    public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
    {
        if (HasCold(npc) && HasToxin(npc))
            damage *= VIRAL_DMG_MULT;
        return true;
    }
}
