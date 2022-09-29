using Terraria.DataStructures;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.Players;

internal class StartingItemsPlayer : ModPlayer
{
    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        return new Item[] { new Item(ModContent.ItemType<MK1Paris>()), new Item(ItemID.WoodenArrow, 100) };
    }
}
