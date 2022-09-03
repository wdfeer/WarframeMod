using Terraria.GameContent.ItemDropRules;
using WarframeMod.Content.Items;
using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalNPCs;

internal class TravellingMerchant : GlobalNPC
{
    public override void SetupTravelShop(int[] shop, ref int nextSlot)
    {
        if (Main.hardMode)
        {
            AddItemToShop(ModContent.ItemType<MaraDetron>(), ref shop, ref nextSlot);
            AddItemToShop(ModContent.ItemType<PrimedReach>(), ref shop, ref nextSlot);
        }
    }
    void AddItemToShop(int itemType, ref int[] shop, ref int nextSlot)
    {
        shop[nextSlot] = itemType;
        nextSlot++;
    }
}
