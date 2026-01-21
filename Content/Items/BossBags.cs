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
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<Tetra>(),
                    ModContent.ItemType<Physique>(),
                    ModContent.ItemType<InfectedClip>(),
                });
            case ItemID.EyeOfCthulhuBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<HunterSynergy>(),
                    ModContent.ItemType<HunterMunitions>(),
                    ModContent.ItemType<Reach>(),
                    ModContent.ItemType<SteelFiber>()
                });
            case ItemID.EaterOfWorldsBossBag or ItemID.BrainOfCthulhuBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<RaktaBallistica>(),
                    ModContent.ItemType<GorgonWraith>(),
                    ModContent.ItemType<Detron>(),
                    ModContent.ItemType<Intensify>()
                });
            case ItemID.SkeletronBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<Desecrate>(),
                    ModContent.ItemType<Hate>(),
                    ModContent.ItemType<Baza>()
                });
            case ItemID.QueenBeeBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<Kohm>(),
                    ModContent.ItemType<MaimingStrike>(),
                    ModContent.ItemType<DetectVulnerability>()
                });
            case ItemID.WallOfFleshBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<Bite>(),
                    ModContent.ItemType<SplitChamber>(),
                    ModContent.ItemType<VitalSense>(),
                    ModContent.ItemType<HighVoltage>(),
                    ModContent.ItemType<Zenistar>()
                });
            case ItemID.QueenSlimeBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<HunterRecovery>(),
                    ModContent.ItemType<HunterCommand>(),
                    ModContent.ItemType<AcceleratedIsotope>(),
                    ModContent.ItemType<Magnetize>(),
                    ModContent.ItemType<Gammacor>()
                });
            case ItemID.DestroyerBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<Opticor>(),
                    ModContent.ItemType<Lecta>()
                });
            case ItemID.SkeletronPrimeBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
                {
                    ModContent.ItemType<Acceltra>()
                });
            case ItemID.TwinsBossBag:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Vaporize>(), 2);
            case ItemID.PlanteraBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(1, new int[]
                {
                    ModContent.ItemType<StradavarPrime>(),
                    ModContent.ItemType<SerratedRounds>()
                });
            case ItemID.GolemBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
                {
                    ModContent.ItemType<BazaPrime>(),
                    ModContent.ItemType<BulletDance>(),
                });
            default:
                return null;
            case ItemID.MoonLordBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
                {
                    ModContent.ItemType<PrismaGorgon>(),
                    ModContent.ItemType<TenetDetron>(),
                    ModContent.ItemType<PrismaLenz>()
                });
        }
    }
    public static IItemDropRule GetArcanesDropRule()
        => ItemDropRule.OneFromOptionsNotScalingWithLuck(2, Arcane.GetArcaneTypes());
}
