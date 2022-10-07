using Terraria.GameContent.ItemDropRules;
using WarframeMod.Content.Items;
using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Common.GlobalNPCs;

internal class NPCLoot : GlobalNPC
{
    public IItemDropRule GetItemDropRule(NPC npc)
    {
        int type = npc.type;
        switch (type)
        {
            case NPCID.GreenSlime or NPCID.BlueSlime:
                return ItemDropRule.Common(ModContent.ItemType<Vitality>(), 250);
            case NPCID.Skeleton or NPCID.SkeletonAlien or NPCID.SkeletonAstonaut:
                return ItemDropRule.Common(ModContent.ItemType<PointStrike>(), 30);
            case NPCID.Harpy:
                return ItemDropRule.OneFromOptions(20, new int[]
                {
                    ModContent.ItemType<MotusSetup>(),
                    ModContent.ItemType<MotusSignal>(),
                });
            case NPCID.UndeadMiner:
                return ItemDropRule.Common(ModContent.ItemType<CriticalDelay>(), 2);
            case NPCID.BloodZombie or NPCID.Drippler:
                return ItemDropRule.Common(ModContent.ItemType<PiercingHit>(), 100);
            case NPCID.FireImp:
                return ItemDropRule.Common(ModContent.ItemType<Blaze>(), 15);
            case NPCID.DarkCaster:
                return ItemDropRule.Common(ModContent.ItemType<NaturalTalent>(), 11);
            default:
                return null;
        }
    }
    IItemDropRule GetEaterOfWorldsDropRule(IItemDropRule normalRule)
    {
        LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
        leadingConditionRule.OnSuccess(normalRule);
        return leadingConditionRule;
    }
    IItemDropRule GetTwinsDropRule(IItemDropRule normalRule)
    {
        LeadingConditionRule leadingConditionRule = new(new Conditions.MissingTwin());
        leadingConditionRule.OnSuccess(normalRule);
        return leadingConditionRule;
    }
    IItemDropRule GetBossDropRule(NPC npc)
    {
        switch (npc.type)
        {
            case NPCID.KingSlime:
                return BossBags.GetGeneralDropRule(ItemID.KingSlimeBossBag);
            case NPCID.EyeofCthulhu:
                return BossBags.GetGeneralDropRule(ItemID.EyeOfCthulhuBossBag);
            case NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail:
                return GetEaterOfWorldsDropRule(BossBags.GetGeneralDropRule(ItemID.EaterOfWorldsBossBag));
            case NPCID.BrainofCthulhu:
                return BossBags.GetGeneralDropRule(ItemID.BrainOfCthulhuBossBag);
            case NPCID.QueenBee:
                return BossBags.GetGeneralDropRule(ItemID.QueenBeeBossBag);
            case NPCID.SkeletronHead:
                return BossBags.GetGeneralDropRule(ItemID.SkeletronBossBag);
            case NPCID.WallofFlesh:
                return BossBags.GetGeneralDropRule(ItemID.WallOfFleshBossBag);
            case NPCID.QueenSlimeBoss:
                return BossBags.GetGeneralDropRule(ItemID.QueenSlimeBossBag);
            case NPCID.TheDestroyer:
                return BossBags.GetGeneralDropRule(ItemID.DestroyerBossBag);
            case NPCID.SkeletronPrime:
                return BossBags.GetGeneralDropRule(ItemID.SkeletronPrimeBossBag);
            case NPCID.Retinazer or NPCID.Spazmatism:
                return GetTwinsDropRule(BossBags.GetGeneralDropRule(ItemID.TwinsBossBag));
            case NPCID.Plantera:
                return BossBags.GetGeneralDropRule(ItemID.PlanteraBossBag);
            case NPCID.HallowBoss:
                return BossBags.GetGeneralDropRule(4782);
            case NPCID.Golem:
                return BossBags.GetGeneralDropRule(ItemID.GolemBossBag);
            case NPCID.DukeFishron:
                return BossBags.GetGeneralDropRule(ItemID.FishronBossBag);
            case NPCID.CultistBoss:
                return BossBags.GetGeneralDropRule(ItemID.CultistBossBag);
            case NPCID.MoonLordCore:
                return BossBags.GetGeneralDropRule(ItemID.MoonLordBossBag);
            default:
                return null;
        }
    }
    public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
    {
        var dropRule = GetItemDropRule(npc);
        if (dropRule != null)
            npcLoot.Add(dropRule);
        dropRule = GetBossDropRule(npc);
        if (dropRule != null)
        {
            LeadingConditionRule normalModeRule = new LeadingConditionRule(new Conditions.NotExpert());
            normalModeRule.OnSuccess(dropRule);
            npcLoot.Add(normalModeRule);
        }
    }
}
