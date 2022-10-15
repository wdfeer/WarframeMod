﻿namespace WarframeMod.Common.GlobalNPCs;
internal class EnemyBuff : GlobalNPC
{
    public const float MAX_LIFE_MULT = 1.05f;
    public const float DEFENSE_MULT = 1.1f;
    public override void SetDefaults(NPC npc)
    {
        base.SetDefaults(npc);
        if (!Main.expertMode)
            return;
        float netMult = Main.netMode == NetmodeID.SinglePlayer ? 1f : 1.05f;
        npc.lifeMax = (int)(npc.lifeMax * MAX_LIFE_MULT * netMult);
        npc.life = (int)(npc.life * MAX_LIFE_MULT * netMult);
        npc.defense = (int)(npc.defense * DEFENSE_MULT * netMult);
    }
    public const float DAMAGE_MULT = 1.1f;
}