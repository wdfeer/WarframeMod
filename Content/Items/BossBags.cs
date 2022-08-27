﻿using Terraria.GameContent.ItemDropRules;
using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items;
internal class BossBags : GlobalItem
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
    IItemDropRule GetGeneralDropRule(int bagType)
    {
        switch (bagType)
        {
            case ItemID.KingSlimeBossBag:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Tetra>(), 2);
            case ItemID.EyeOfCthulhuBossBag:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<HunterMunitions>(), 2);
            case ItemID.EaterOfWorldsBossBag or ItemID.BrainOfCthulhuBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
                {
                    ModContent.ItemType<RaktaBallistica>(),
                    ModContent.ItemType<GorgonWraith>()
                });
            case ItemID.SkeletronBossBag:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Desecrate>(), 3);
            case ItemID.QueenBeeBossBag:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Kohm>(), 2);
            case ItemID.WallOfFleshBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
                {
                    ModContent.ItemType<Bite>(),
                    ModContent.ItemType<SplitChamber>(),
                    ModContent.ItemType<Opticor>()
                });
            case ItemID.SkeletronPrimeBossBag:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Magnetize>(), 3);
            case ItemID.DestroyerBossBag:
                return ItemDropRule.NotScalingWithLuck(ModContent.ItemType<KuvaNukor>(), 2);
            case ItemID.TwinsBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
                {
                    ModContent.ItemType<Vaporize>(),
                    ModContent.ItemType<Acceltra>()
                });
            case ItemID.GolemBossBag:
                return ItemDropRule.OneFromOptionsNotScalingWithLuck(2, new int[]
                {
                    ModContent.ItemType<Supra>(),
                    ModContent.ItemType<BazaPrime>()
                });
            default:
                return null;
        }
    }
    IItemDropRule GetArcanesDropRule()
        => ItemDropRule.OneFromOptionsNotScalingWithLuck(
            4,
            new int[]
            {
                ModContent.ItemType<ArcaneAvenger>(),
                ModContent.ItemType<ArcaneGuardian>(),
                ModContent.ItemType<VirtuosStrike>(),
                ModContent.ItemType<ArcaneStrike>(),
                ModContent.ItemType<ArcanePrecision>(),
                ModContent.ItemType<ArcaneFury>(),
                ModContent.ItemType<ArcaneBodyguard>(),
                ModContent.ItemType<ArcaneArachne>(),
                ModContent.ItemType<ArcaneVictory>()
            });
}
