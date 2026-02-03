using System.Diagnostics;
using Terraria.GameContent.ItemDropRules;
using WarframeMod.Content.Items;
using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Arcanes;
using WarframeMod.Content.Items.Consumables;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalNPCs;

internal class NPCLoot : GlobalNPC
{
    public static Dictionary<int, IItemDropRule[]> dropRules;

    public override void SetStaticDefaults()
    {
        Dictionary<int, List<IItemDropRule>> rules = new();

        void Add(IItemDropRule rule, params int[] npcs)
        {
            Debug.Assert(npcs.Any());
            foreach (int npc in npcs)
            {
                if (!rules.TryGetValue(npc, out var list))
                {
                    list = new List<IItemDropRule>();
                    rules[npc] = list;
                }

                list.Add(rule);
            }
        }

        void AddSimple(int itemType, int denominator, params int[] npcs)
            => Add(ItemDropRule.Common(itemType, denominator), npcs);

        void AddConditional(IItemDropRuleCondition condition, int itemType, int denominator, params int[] npcs)
            => Add(ItemDropRule.ByCondition(condition, itemType, denominator), npcs);

        void AddExpert(int itemType, int denominator, params int[] npcs)
            => AddConditional(new Conditions.IsExpert(), itemType, denominator, npcs);

        // Slimes
        AddSimple(ModContent.ItemType<Vitality>(), 200, NPCID.GreenSlime, NPCID.BlueSlime);

        // Corruption / Crimson early
        AddConditional(new GrimoireDropCondition(), ModContent.ItemType<Grimoire>(), 25,
            NPCID.EaterofSouls, NPCID.DevourerHead, NPCID.BloodCrawler, NPCID.Crimera);

        AddSimple(ModContent.ItemType<Furis>(), 40, NPCID.JungleBat);
        Add(ItemDropRule.ByCondition(new BeatQueenBeeCondition(), ModContent.ItemType<Pyrana>(), 60), NPCID.Piranha);

        // Skeletons
        AddSimple(ModContent.ItemType<PointStrike>(), 15,
            NPCID.Skeleton, NPCID.SkeletonAlien, NPCID.SkeletonAstonaut, NPCID.SkeletonTopHat,
            NPCID.BoneThrowingSkeleton, NPCID.BoneThrowingSkeleton2);

        Add(ItemDropRule.OneFromOptions(30,
                ModContent.ItemType<MotusSetup>(),
                ModContent.ItemType<MotusSignal>()),
            NPCID.Harpy);

        AddExpert(ModContent.ItemType<ExodiaValor>(), 30, NPCID.GreekSkeleton);

        // Beetles
        AddConditional(new GrimoireUpgradeDropCondition(), ModContent.ItemType<JahuCanticle>(), 2,
            NPCID.CochinealBeetle, NPCID.CyanBeetle, NPCID.LacBeetle);

        AddSimple(ModContent.ItemType<PiercingHit>(), 80, NPCID.BloodZombie, NPCID.Drippler);
        AddSimple(ModContent.ItemType<Blaze>(), 25, NPCID.FireImp);

        Add(ItemDropRule.OneFromOptions(15,
                ModContent.ItemType<NaturalTalent>(),
                ModContent.ItemType<Simulor>()),
            NPCID.DarkCaster);

        AddSimple(ModContent.ItemType<Kuva>(), 15,
            NPCID.Corruptor, NPCID.CorruptSlime, NPCID.Slimer, NPCID.CursedHammer, NPCID.Clinger,
            NPCID.PigronCorruption, NPCID.DarkMummy, NPCID.DesertGhoulCorruption, NPCID.Herpling,
            NPCID.Crimslime, NPCID.BloodJelly, NPCID.CrimsonAxe, NPCID.IchorSticker,
            NPCID.FloatyGross, NPCID.PigronCrimson, NPCID.BloodMummy, NPCID.DesertGhoulCrimson);

        AddSimple(ModContent.ItemType<BuzzKill>(), 33, NPCID.BloodFeeder, NPCID.CorruptGoldfish);
        Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Kuva>(), 3), NPCID.BigMimicCorruption,
            NPCID.BigMimicCrimson);

        Add(ItemDropRule.OneFromOptions(30,
                ModContent.ItemType<Tonkor>(),
                ModContent.ItemType<Seer>(),
                ModContent.ItemType<Cronus>()),
            NPCID.GoblinPeon, NPCID.GoblinSorcerer, NPCID.GoblinThief,
            NPCID.GoblinWarrior, NPCID.GoblinArcher);

        AddExpert(ModContent.ItemType<MagusAggress>(), 200, NPCID.ChaosElemental);
        AddExpert(ModContent.ItemType<LongbowSharpshot>(), 150, NPCID.SkeletonArcher, NPCID.ElfArcher);

        AddSimple(ModContent.ItemType<Rubico>(), 50, NPCID.PirateCorsair);
        AddExpert(ModContent.ItemType<ExodiaForce>(), 4, NPCID.GoblinShark, NPCID.BloodEelHead);
        AddSimple(ModContent.ItemType<ShatteringJustice>(), 3, NPCID.BloodNautilus);

        Add(ItemDropRule.OneFromOptions(2,
                ModContent.ItemType<EnergyGenerator>(),
                ModContent.ItemType<Guandao>()),
            NPCID.SandElemental);

        AddSimple(ModContent.ItemType<HealingReturn>(), 50, NPCID.Unicorn, NPCID.Gastropod);
        AddConditional(new GrimoireUpgradeDropCondition(), ModContent.ItemType<LohkCanticle>(), 50,
            NPCID.DD2DrakinT2, NPCID.DD2OgreT2, NPCID.DD2LightningBugT3);

        Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Fieldron>(), 4), NPCID.MartianSaucerCore);

        AddSimple(ModContent.ItemType<Fieldron>(), 250,
            NPCID.Scutlix, NPCID.MartianWalker, NPCID.MartianDrone, NPCID.MartianEngineer,
            NPCID.MartianOfficer, NPCID.MartianTurret, NPCID.GigaZapper,
            NPCID.RayGunner, NPCID.GrayGrunt, NPCID.BrainScrambler);

        AddExpert(ModContent.ItemType<VirtuosTrojan>(), 4, NPCID.QueenBee);

        AddConditional(new GrimoireUpgradeDropCondition(), ModContent.ItemType<VomeInvocation>(), 1, NPCID.WallofFlesh);
        AddConditional(new DreadDropCondition(), ModContent.ItemType<Dread>(), 1, NPCID.WallofFlesh);

        AddExpert(ModContent.ItemType<PaxSoar>(), 2, NPCID.QueenSlimeBoss);
        AddExpert(ModContent.ItemType<ResidualShock>(), 2, NPCID.TheDestroyer);

        AddConditional(new GrimoireDropCondition(), ModContent.ItemType<XataInvocation>(), 1, NPCID.Plantera);
        AddExpert(ModContent.ItemType<VirtuosTrojan>(), 6, NPCID.Plantera);

        AddConditional(new GrimoireDropCondition(), ModContent.ItemType<RisInvocation>(), 1, NPCID.HallowBoss);

        dropRules = rules.ToDictionary(pair => pair.Key, pair => pair.Value.ToArray());
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
        {
            // Normal drop rules
            var rules = dropRules[npc.type];
            if (rules != null)
                foreach (IItemDropRule rule in rules)
                {
                    npcLoot.Add(rule);
                }
        }

        {
            // Loot from boss bags in normal mode
            IItemDropRule rule = GetBossDropRule(npc);
            if (rule != null)
            {
                LeadingConditionRule normalModeRule = new LeadingConditionRule(new Conditions.NotExpert());
                normalModeRule.OnSuccess(rule);
                npcLoot.Add(normalModeRule);
            }
        }

        {
            // Dragon key drop rules
            IItemDropRule rule = GetDragonKeyDropRule(npc);

            if (rule != null)
                npcLoot.Add(rule);
        }
    }
}