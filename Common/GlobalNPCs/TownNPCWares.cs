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
                if (NPC.downedBoss1)
                    AddShopItem(ModContent.ItemType<Paris>(), shop, ref nextSlot).shopCustomPrice = Item.buyPrice(gold: 1);
                return;
            case NPCID.GoblinTinkerer:
                AddShopItem(ModContent.ItemType<Ballistica>(), shop, ref nextSlot);
                return;
            default:
                return;
        }
    }
    Item AddShopItem(int itemType, Chest shop, ref int nextSlot)
    {
        Item item = new Item(itemType);
        shop.item[nextSlot] = item;
        nextSlot++;
        return item;
    }
}
