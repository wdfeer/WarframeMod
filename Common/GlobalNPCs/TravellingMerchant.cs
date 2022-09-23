using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalNPCs;

internal class TravellingMerchant : GlobalNPC
{
    public override void SetupTravelShop(int[] shop, ref int nextSlot)
    {
        if (!Main.hardMode)
            return;
        AddItemToShop(ModContent.ItemType<PrimedReach>(), ref shop, ref nextSlot);
        if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
        {
            AddItemToShop(ModContent.ItemType<MaraDetron>(), ref shop, ref nextSlot);
            AddItemToShop(ModContent.ItemType<PrismaTetra>(), ref shop, ref nextSlot);
            if (NPC.downedMoonlord)
            {
                AddItemToShop(ModContent.ItemType<PrismaGorgon>(), ref shop, ref nextSlot);
            }
        }
    }
    void AddItemToShop(int itemType, ref int[] shop, ref int nextSlot)
    {
        shop[nextSlot] = itemType;
        nextSlot++;
    }
}
