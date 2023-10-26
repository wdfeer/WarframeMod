using WarframeMod.Common.Configs;

namespace WarframeMod.Common.GlobalNPCs;
internal class EnemyBuff : GlobalNPC
{
    public static float GetMaxLifeMult() => ModContent.GetInstance<WarframeServerConfig>().enemyMaxLifeIncreasePercent / 100f + 1f;
    public static float GetDefenseMult() => ModContent.GetInstance<WarframeServerConfig>().enemyDefenseIncreasePercent / 100f + 1f;
    public override void SetDefaults(NPC npc)
    {
        base.SetDefaults(npc);
        if (!Main.expertMode)
            return;
        npc.lifeMax = (int)(npc.lifeMax * GetMaxLifeMult());
        npc.life = (int)(npc.life * GetMaxLifeMult());
        npc.defense = (int)(npc.defense * GetDefenseMult());
    }
    public static float GetDamageMult() => ModContent.GetInstance<WarframeServerConfig>().enemyDamageIncreasePercent / 100f + 1f;
}
