using Terraria;

namespace WarframeMod
{
    public struct BuffChance
    {
        int type;
        int time;
        float chance;
        public BuffChance(int type, int time, float chance)
        {
            this.type = type;
            this.time = time;
            this.chance = chance;
        }
        public void RollAndApply(NPC npc)
        {
            if (Main.rand.NextFloat() > chance)
                return;
            npc.AddBuff(type, time);
        }
    }
}
