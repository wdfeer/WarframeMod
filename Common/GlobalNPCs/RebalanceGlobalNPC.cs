using WarframeMod.Common.Configs;

namespace WarframeMod.Common.GlobalNPCs;
internal class RebalanceGlobalNPC : GlobalNPC
{
    public static float GetMaxLifeMult() => ModContent.GetInstance<WarframeServerConfig>().enemyMaxLifeIncreasePercent / 100f + 1f;
    public static float GetDefenseMult() => ModContent.GetInstance<WarframeServerConfig>().enemyDefenseIncreasePercent / 100f + 1f;
    public override void SetDefaults(NPC npc)
    {
        base.SetDefaults(npc);
        if (!Main.expertMode || !ModContent.GetInstance<WarframeServerConfig>().enableStatChanges)
            return;
        npc.lifeMax = (int)(npc.lifeMax * GetMaxLifeMult());
        npc.life = (int)(npc.life * GetMaxLifeMult());
        npc.defense = (int)(npc.defense * GetDefenseMult());
    }
    public static float GetDamageMult()
        => Main.expertMode ? (ModContent.GetInstance<WarframeServerConfig>().enemyDamageIncreasePercent / 100f + 1f) : 1f;
}
