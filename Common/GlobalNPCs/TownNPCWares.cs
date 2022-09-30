using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalNPCs;

internal class TownNPCWares : GlobalNPC
{
    public override void SetupShop(int type, Chest shop, ref int nextSlot)
    {
        switch (type)
        {
            case NPCID.Merchant:
                AddShopItem(ModContent.ItemType<MK1Paris>(), shop, ref nextSlot);
                return;
            case NPCID.GoblinTinkerer:
                AddShopItem(ModContent.ItemType<Ballistica>(), shop, ref nextSlot);
                return;
            default:
                return;
        }
    }
    void AddShopItem(int itemType, Chest shop, ref int nextSlot)
    {
        shop.item[nextSlot] = new Item(itemType);
        nextSlot++;
    }
}
