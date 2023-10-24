using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalNPCs;

internal class TownNPCWares : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        switch (shop.NpcType)
        {
            case NPCID.Merchant:
                shop.Add(ModContent.ItemType<MK1Paris>());
                shop.Add(ModContent.ItemType<MK1Bo>());
                return;
            case NPCID.ArmsDealer:
                shop.Add(ModContent.ItemType<Burston>());
                return;
            case NPCID.GoblinTinkerer:
                shop.Add(ModContent.ItemType<Ballistica>());
                shop.Add(ModContent.ItemType<Tonkor>());
                return;
            case NPCID.Mechanic:
                shop.Add(ModContent.ItemType<Spectra>());
                return;
            default:
                return;
        }
    }
}
