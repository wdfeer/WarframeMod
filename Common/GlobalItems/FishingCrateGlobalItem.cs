using Terraria.GameContent.ItemDropRules;
using WarframeMod.Content.Items.Arcanes;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalItems;

public class FishingCrateGlobalItem : GlobalItem
{
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        if (ItemID.Sets.IsFishingCrate[item.type])
        {
            foreach (var rule in GetGenericCrateDropRules())
            {
                itemLoot.Add(rule);
            }
        }
    }

    public List<IItemDropRule> GetGenericCrateDropRules()
    {
        var list = new List<IItemDropRule>();
        
        list.Add(ItemDropRule.Common(ModContent.ItemType<PlasmaSword>(), 20));
        
        list.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(),
            ModContent.ItemType<MagusVigor>(), 12));
        list.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(),
            ModContent.ItemType<ArcaneAgility>(), 50));

        return list;
    }
}