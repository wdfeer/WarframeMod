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

        // Generic helper: add any IItemDropRule to NPC(s)
        void Add(IItemDropRule rule, params int[] npcs)
        {
            Debug.Assert(npcs.Length > 0);
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

        // Generic: add a simple common drop
        void AddSimple<T>(int denominator, params int[] npcs) where T : ModItem
            => Add(ItemDropRule.Common(ModContent.ItemType<T>(), denominator), npcs);

        // Generic: add a drop with a condition
        void AddConditional<T>(IItemDropRuleCondition condition, int denominator, params int[] npcs) where T : ModItem
            => Add(ItemDropRule.ByCondition(condition, ModContent.ItemType<T>(), denominator), npcs);

        // Generic: add an expert-mode only drop
        void AddExpert<T>(int denominator, params int[] npcs) where T : ModItem
            => AddConditional<T>(new Conditions.IsExpert(), denominator, npcs);

        void AddOneFromOptions(int denominator, int[] npcs, params int[] itemTypes)
            => Add(ItemDropRule.OneFromOptions(denominator, itemTypes), npcs);

        void AddOneFromOptions2<T1, T2>(int denominator, params int[] npcs)
            where T1 : ModItem
            where T2 : ModItem
            => AddOneFromOptions(denominator, npcs, ModContent.ItemType<T1>(), ModContent.ItemType<T2>());

        void AddOneFromOptions3<T1, T2, T3>(int denominator, params int[] npcs)
            where T1 : ModItem
            where T2 : ModItem
            where T3 : ModItem
            => AddOneFromOptions(denominator, npcs, ModContent.ItemType<T1>(), ModContent.ItemType<T2>(),
                ModContent.ItemType<T3>());

        // === Pre-Hardmode / Both PH & HM ===

        // Slimes
        AddSimple<Kunai>(50, NPCID.PurpleSlime, NPCID.YellowSlime);
        AddSimple<Vitality>(200, NPCID.GreenSlime, NPCID.BlueSlime);

        // Corruption / Crimson early
        AddConditional<Grimoire>(new GrimoireDropCondition(), 25,
            NPCID.EaterofSouls, NPCID.DevourerHead, NPCID.BloodCrawler, NPCID.Crimera);

        AddSimple<Furis>(40, NPCID.JungleBat);
        AddConditional<Pyrana>(new BeatQueenBeeCondition(), 60, NPCID.Piranha);

        // Skeletons
        AddSimple<PointStrike>(15,
            NPCID.Skeleton, NPCID.SkeletonAlien, NPCID.SkeletonAstonaut, NPCID.SkeletonTopHat,
            NPCID.BoneThrowingSkeleton, NPCID.BoneThrowingSkeleton2);

        AddOneFromOptions2<MotusSetup, MotusSignal>(30, NPCID.Harpy);

        AddExpert<ExodiaValor>(30, NPCID.GreekSkeleton);

        // Beetles
        AddConditional<JahuCanticle>(new GrimoireUpgradeDropCondition(), 2,
            NPCID.CochinealBeetle, NPCID.CyanBeetle, NPCID.LacBeetle);

        AddSimple<PiercingHit>(80, NPCID.BloodZombie, NPCID.Drippler);
        AddSimple<Blaze>(25, NPCID.FireImp);

        // Goblins
        AddOneFromOptions3<Tonkor, Seer, Cronus>(30,
            NPCID.GoblinPeon, NPCID.GoblinSorcerer, NPCID.GoblinThief,
            NPCID.GoblinWarrior, NPCID.GoblinArcher);

        AddOneFromOptions2<NaturalTalent, Simulor>(15, NPCID.DarkCaster);

        AddSimple<BuzzKill>(33, NPCID.BloodFeeder, NPCID.CorruptGoldfish);

        AddExpert<VirtuosTrojan>(4, NPCID.QueenBee);

        // === Hardmode ===

        AddConditional<VomeInvocation>(new GrimoireUpgradeDropCondition(), 1, NPCID.WallofFlesh);
        AddConditional<Dread>(new DreadDropCondition(), 1, NPCID.WallofFlesh);

        Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Kuva>(), 3), NPCID.BigMimicCorruption,
            NPCID.BigMimicCrimson);
        AddConditional<Kuva>(new Conditions.IsHardmode(), 15,
            NPCID.Corruptor, NPCID.CorruptSlime, NPCID.Slimer, NPCID.CursedHammer, NPCID.Clinger,
            NPCID.PigronCorruption, NPCID.DarkMummy, NPCID.DesertGhoulCorruption, NPCID.Herpling,
            NPCID.Crimslime, NPCID.BloodJelly, NPCID.CrimsonAxe, NPCID.IchorSticker,
            NPCID.FloatyGross, NPCID.PigronCrimson, NPCID.BloodMummy, NPCID.DesertGhoulCrimson);

        AddExpert<ArcaneIntention>(200, NPCID.Arapaima, NPCID.GiantFlyingFox);

        AddExpert<MagusAggress>(200, NPCID.ChaosElemental);
        AddExpert<MagusCadence>(200, NPCID.ChaosElemental);

        AddExpert<LongbowSharpshot>(150, NPCID.SkeletonArcher, NPCID.ElfArcher);

        AddSimple<Rubico>(50, NPCID.PirateCorsair);
        AddSimple<StockpiledBlight>(150, NPCID.PirateGhost, NPCID.PirateDeckhand);
        
        AddExpert<ExodiaForce>(4, NPCID.GoblinShark, NPCID.BloodEelHead);
        AddSimple<ShatteringJustice>(3, NPCID.BloodNautilus);

        AddOneFromOptions2<EnergyGenerator, Guandao>(2, NPCID.SandElemental);
        AddExpert<FractalizedReset>(15, NPCID.SandElemental, NPCID.IceGolem);

        AddExpert<PaxCharge>(200, NPCID.Pixie, NPCID.Gastropod);
        AddSimple<HealingReturn>(50, NPCID.Unicorn, NPCID.Gastropod);

        AddExpert<PaxSoar>(3, NPCID.QueenSlimeBoss);
        AddExpert<ResidualShock>(3, NPCID.TheDestroyer);

        AddExpert<ResidualBoils>(120, NPCID.Lavabat, NPCID.RedDevil);

        AddConditional<LohkCanticle>(new GrimoireUpgradeDropCondition(), 50,
            NPCID.DD2DrakinT2, NPCID.DD2OgreT2, NPCID.DD2LightningBugT3);

        // === Post-Plantera ===

        AddConditional<XataInvocation>(new GrimoireUpgradeDropCondition(), 1, NPCID.Plantera);
        AddExpert<VirtuosTrojan>(6, NPCID.Plantera);

        AddSimple<Hind>(12, NPCID.HeadlessHorseman);
        AddSimple<DotdSarpa>(10, NPCID.Mothron, NPCID.MourningWood);
        AddSimple<DotdTonkor>(10, NPCID.Mothron, NPCID.Pumpking);
        AddExpert<MoltEfficiency>(200, NPCID.Poltergeist, NPCID.DeadlySphere);

        // === Post-Golem ===

        Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Fieldron>(), 4), NPCID.MartianSaucerCore);
        AddSimple<Fieldron>(250,
            NPCID.Scutlix, NPCID.MartianWalker, NPCID.MartianDrone, NPCID.MartianEngineer,
            NPCID.MartianOfficer, NPCID.MartianTurret, NPCID.GigaZapper,
            NPCID.RayGunner, NPCID.GrayGrunt, NPCID.BrainScrambler);

        AddSimple<CernosPrime>(10, NPCID.DD2Betsy);

        AddConditional<RisInvocation>(new GrimoireUpgradeDropCondition(), 2, NPCID.HallowBoss);

        AddExpert<ArcanePersistence>(7, NPCID.HallowBoss, NPCID.DukeFishron);

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
        if (dropRules.ContainsKey(npc.type)) // Normal drop rules
        {
            var rules = dropRules[npc.type];
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