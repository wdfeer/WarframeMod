using Terraria.GameContent.ItemDropRules;
using WarframeMod.Items.Accessories;
using WarframeMod.Items.Weapons;

namespace WarframeMod.Global;

internal class NPCLoot : GlobalNPC
{
    public IItemDropRule GetItemDropRule(NPC npc)
    {
        int type = npc.type;
        switch (type)
        {
            case NPCID.Skeleton or NPCID.SkeletonAlien or NPCID.SkeletonAstonaut:
                return ItemDropRule.Common(ModContent.ItemType<PointStrike>(), 30);
            case NPCID.CaveBat or NPCID.JungleBat:
                return ItemDropRule.Common(ModContent.ItemType<MotusSetup>(), 80);
            case NPCID.UndeadMiner:
                return ItemDropRule.Common(ModContent.ItemType<CriticalDelay>(), 2);
            case NPCID.BloodZombie or NPCID.Drippler:
                return ItemDropRule.Common(ModContent.ItemType<PiercingHit>(), 100);
            case NPCID.FireImp:
                return ItemDropRule.Common(ModContent.ItemType<Blaze>(), 16);
            default:
                return null;
        }
    }
    public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
    {
        var dropRule = GetItemDropRule(npc);
        if (dropRule != null)
            npcLoot.Add(dropRule);
    }
}
