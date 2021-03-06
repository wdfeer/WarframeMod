using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Items.Accessories;
using WarframeMod.Items.Weapons;

namespace WarframeMod.Global
{
    internal class NPCLoot : GlobalNPC
    {
        public IItemDropRule GetItemDropRule(int type)
        {
            switch (type)
            {
                case NPCID.BloodZombie or NPCID.Drippler:
                    return ItemDropRule.Common(ModContent.ItemType<PiercingHit>(), 100);
                case NPCID.FireImp:
                    return ItemDropRule.Common(ModContent.ItemType<Blaze>(), 16);
                case NPCID.EyeofCthulhu:
                    return ItemDropRule.Common(ModContent.ItemType<HunterMunitions>(), 2);
                case NPCID.SkeletronHead:
                    return ItemDropRule.Common(ModContent.ItemType<Desecrate>(), 3);
                case NPCID.QueenBee:
                    return ItemDropRule.Common(ModContent.ItemType<Kohm>(), 2);
                case NPCID.SkeletronPrime:
                    return ItemDropRule.Common(ModContent.ItemType<Magnetize>(), 3);
                case NPCID.TheDestroyer:
                    return ItemDropRule.Common(ModContent.ItemType<KuvaNukor>(), 2);
                default:
                    return null;
            }
        }
        public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            var dropRule = GetItemDropRule(npc.type);
            if (dropRule != null)
                npcLoot.Add(dropRule);
        }
    }
}
