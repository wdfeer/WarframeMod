using Terraria.GameContent.ItemDropRules;
using WarframeMod.Content.Items;
using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalNPCs;

internal class NPCLoot : GlobalNPC
{
    static IItemDropRule GetItemDropRule(NPC npc)
    {
        int type = npc.type;
        switch (type)
        {
            case NPCID.GreenSlime or NPCID.BlueSlime:
                return ItemDropRule.Common(ModContent.ItemType<Vitality>(), 200);
            case NPCID.JungleBat:
                return ItemDropRule.Common(ModContent.ItemType<Furis>(), 40);
            case NPCID.Skeleton or NPCID.SkeletonAlien or NPCID.SkeletonAstonaut or NPCID.SkeletonTopHat or NPCID.BoneThrowingSkeleton or NPCID.BoneThrowingSkeleton2:
                return ItemDropRule.Common(ModContent.ItemType<PointStrike>(), 15);
            case NPCID.Harpy:
                return ItemDropRule.OneFromOptions(30,
                [
                    ModContent.ItemType<MotusSetup>(),
                    ModContent.ItemType<MotusSignal>(),
                ]);
            case NPCID.BloodZombie or NPCID.Drippler:
                return ItemDropRule.Common(ModContent.ItemType<PiercingHit>(), 80);
            case NPCID.FireImp:
                return ItemDropRule.Common(ModContent.ItemType<Blaze>(), 25);
            case NPCID.DarkCaster:
                return ItemDropRule.OneFromOptions(15,
                [
                    ModContent.ItemType<NaturalTalent>(),
                    ModContent.ItemType<Simulor>()
                ]);
            case NPCID.Corruptor or NPCID.CorruptSlime or NPCID.Slimer or NPCID.CursedHammer or NPCID.Clinger or NPCID.PigronCorruption or NPCID.DarkMummy or NPCID.DesertGhoulCorruption or
                 NPCID.Herpling or NPCID.Crimslime or NPCID.BloodJelly or NPCID.BloodFeeder or NPCID.CrimsonAxe or NPCID.IchorSticker or NPCID.FloatyGross or NPCID.PigronCrimson or NPCID.BloodMummy or NPCID.DesertGhoulCrimson:
                return ItemDropRule.Common(ModContent.ItemType<Kuva>(), 15);
            case NPCID.BigMimicCorruption or NPCID.BigMimicCrimson:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Kuva>(), 3);

            case NPCID.GoblinPeon or NPCID.GoblinSorcerer or NPCID.GoblinThief or NPCID.GoblinWarrior or NPCID.GoblinArcher:
                return ItemDropRule.OneFromOptions(30,
                [
                    ModContent.ItemType<Tonkor>(),
                    ModContent.ItemType<Seer>(),
                    ModContent.ItemType<Cronus>()
                ]);
            case NPCID.MartianSaucerCore:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Fieldron>(), 4);
            case NPCID.Scutlix or NPCID.MartianWalker or NPCID.MartianDrone or NPCID.MartianEngineer or NPCID.MartianOfficer or NPCID.MartianTurret or NPCID.GigaZapper or NPCID.RayGunner or NPCID.GrayGrunt or NPCID.BrainScrambler:
                return ItemDropRule.Common(ModContent.ItemType<Fieldron>(), 250);
            default:
                return null;
        }
    }

    static IItemDropRule GetEaterOfWorldsDropRule(IItemDropRule normalRule)
    {
        LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
        leadingConditionRule.OnSuccess(normalRule);
        return leadingConditionRule;
    }

    static IItemDropRule GetTwinsDropRule(IItemDropRule normalRule)
    {
        LeadingConditionRule leadingConditionRule = new(new Conditions.MissingTwin());
        leadingConditionRule.OnSuccess(normalRule);
        return leadingConditionRule;
    }

    static IItemDropRule GetBossDropRule(NPC npc)
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

    static IItemDropRule GetDragonKeyDropRule(NPC npc)
    {
        if (npc.boss)
        {
            LeadingConditionRule dragonKeyRule = new LeadingConditionRule(new DragonKeyCondition());
            dragonKeyRule.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
            [
                ModContent.ItemType<CriticalDelay>(),
                ModContent.ItemType<VileAcceleration>(),
                ModContent.ItemType<HollowPoint>(),
                ModContent.ItemType<SpoiledStrike>(),
            ]));
            return dragonKeyRule;
        }
        return null;
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
        dropRule = GetDragonKeyDropRule(npc);
        if (dropRule != null)
            npcLoot.Add(dropRule);
    }
}