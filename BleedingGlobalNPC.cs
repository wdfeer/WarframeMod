using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod
{
    internal class BleedingGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public List<BleedingBuff> bleeds = new();
        const int bleedTickTime = 60;
        int bleedDPS = 0;
        int BleedDamagePerTick => bleedDPS * bleedTickTime / 60;
        int bleedTimer = 0;
        void ResetBleeds()
        {
            bleeds = new();
            bleedDPS = 0;
            bleedTimer = 0;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (!npc.HasBuff(BuffID.Bleeding))
            {
                ResetBleeds();
                return;
            }
            if (npc.lifeRegen > 0)
                npc.lifeRegen = 0;
            bleeds = BleedingBuff.UpdateBleeds(bleeds, out bleedDPS);
            if (bleedTimer % bleedTickTime == 0)
            {
                int oldDefense = npc.defense;
                npc.defense = 0;
                npc.StrikeNPC(BleedDamagePerTick, 0, -2);
                npc.defense = oldDefense;
                bleedTimer = 0;
            }
            bleedTimer++;
        }
    }
    public struct BleedingBuff
    {
        public int dps;
        public int timeLeft;
        public BleedingBuff(int dps, int time)
        {
            this.dps = dps;
            timeLeft = time;
        }
        public static List<BleedingBuff> UpdateBleeds(IEnumerable<BleedingBuff> bleeds, out int damage)
        {
            List<BleedingBuff> newBleeds = new List<BleedingBuff>();
            damage = 0;
            foreach (var item in bleeds)
            {
                damage += item.dps;
                if (item.timeLeft > 0)
                {
                    BleedingBuff newBleed = item;
                    newBleed.timeLeft--;
                    newBleeds.Add(newBleed);
                }
            }
            return newBleeds;
        }
    }
}
