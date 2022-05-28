using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod
{
    internal class NPCLoot : GlobalNPC
    {
        public IItemDropRule GetItemDropRuleForType(int type)
        {
            switch (type)
            {
                case NPCID.SkeletronPrime:
                    return ItemDropRule.Common(ModContent.ItemType<Items.Magnetize>(), 10);
                default:
                    return null;
            }
        }
        public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            var dropRule = GetItemDropRuleForType(npc.type);
            if (dropRule != null)
                npcLoot.Add(dropRule);
        }
    }
}
