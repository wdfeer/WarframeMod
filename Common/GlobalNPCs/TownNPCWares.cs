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
                shop.item[nextSlot] = new Item(ModContent.ItemType<MK1Paris>());
                nextSlot++;
                return;
            default:
                return;
        }
    }
}
