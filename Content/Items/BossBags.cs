using Terraria.GameContent.ItemDropRules;
using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Accessories.Auras;
using WarframeMod.Content.Items.Arcanes;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items;

public class BossBags : GlobalItem
{
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        if (!ItemID.Sets.BossBag[item.type] && !ItemID.Sets.PreHardmodeLikeBossBag[item.type])
            return;
        IItemDropRule extraDrop = GetGeneralDropRule(item.type);
        if (extraDrop is not null)
            itemLoot.Add(extraDrop);
        itemLoot.Add(GetArcanesDropRule());
    }

    public static IItemDropRule GetGeneralDropRule(int bagType)
    {
        switch (bagType)
        {
            case ItemID.KingSlimeBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<Tetra>(),
                    ModContent.ItemType<Physique>(),
                    ModContent.ItemType<InfectedClip>(),
                    ModContent.ItemType<Hikou>());
            case ItemID.EyeOfCthulhuBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<HunterSynergy>(),
                    ModContent.ItemType<HunterMunitions>(),
                    ModContent.ItemType<Reach>(),
                    ModContent.ItemType<SteelFiber>());
            case ItemID.EaterOfWorldsBossBag or ItemID.BrainOfCthulhuBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<RaktaBallistica>(),
                    ModContent.ItemType<GorgonWraith>(),
                    ModContent.ItemType<Detron>(),
                    ModContent.ItemType<Intensify>());
            case ItemID.SkeletronBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<Desecrate>(),
                    ModContent.ItemType<Hate>(),
                    ModContent.ItemType<Baza>());
            case ItemID.QueenBeeBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<Kohm>(),
                    ModContent.ItemType<MaimingStrike>(),
                    ModContent.ItemType<DetectVulnerability>());
            case ItemID.WallOfFleshBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<Bite>(),
                    ModContent.ItemType<SplitChamber>(),
                    ModContent.ItemType<VitalSense>(),
                    ModContent.ItemType<HighVoltage>(),
                    ModContent.ItemType<Zenistar>());
            case ItemID.QueenSlimeBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<HunterRecovery>(),
                    ModContent.ItemType<HunterCommand>(),
                    ModContent.ItemType<Gammacor>(),
                    ModContent.ItemType<EnergyConversion>());
            case ItemID.DestroyerBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<Opticor>(),
                    ModContent.ItemType<Lecta>());
            case ItemID.SkeletronPrimeBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<Acceltra>(),
                    ModContent.ItemType<Magnetize>()
                );
            case ItemID.TwinsBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<Vaporize>(),
                    ModContent.ItemType<AcceleratedIsotope>()
                );
            case ItemID.PlanteraBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1,
                    ModContent.ItemType<StradavarPrime>(),
                    ModContent.ItemType<SerratedRounds>());
            case ItemID.GolemBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2,
                    ModContent.ItemType<BazaPrime>(),
                    ModContent.ItemType<BulletDance>());
            case ItemID.MoonLordBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2,
                    ModContent.ItemType<PrismaGorgon>(),
                    ModContent.ItemType<TenetDetron>(),
                    ModContent.ItemType<PrismaLenz>());
            default:
                return null;
        }
    }

    public static IItemDropRule GetArcanesDropRule()
        => ItemDropRule.OneFromOptionsNotScalingWithLuck(2, Arcane.GetArcaneTypesFromBosses());
}