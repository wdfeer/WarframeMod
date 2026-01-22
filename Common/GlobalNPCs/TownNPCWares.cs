using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common.GlobalNPCs;

internal class TownNPCWares : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        switch (shop.NpcType)
        {
            case NPCID.Merchant:
                shop.Add<MK1Paris>();
                shop.Add<MK1Bo>();
                return;
            case NPCID.ArmsDealer:
                shop.Add<Burston>();
                return;
            case NPCID.GoblinTinkerer:
                shop.Add<Ballistica>();
                shop.Add<Tonkor>();
                return;
            case NPCID.Mechanic:
                shop.Add<Spectra>();
                return;
            case NPCID.SkeletonMerchant:
                shop.Add<Snipetron>(Condition.DownedSkeletron);
                return;
            default:
                return;
        }
    }
}
