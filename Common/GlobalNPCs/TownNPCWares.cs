using Terraria.Enums;
using WarframeMod.Content.Items.Accessories;
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
                shop.Add<MK1Kunai>();
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
                shop.Add<AstralTwilight>(Condition.MoonPhaseWaxingGibbous);
                shop.Add<AstralTwilight>(Condition.MoonPhaseFull);
                shop.Add<AstralTwilight>(Condition.MoonPhaseWaningGibbous);
                return;
            case NPCID.WitchDoctor:
                shop.Add<AnabolicPollination>(Condition.DownedPlantera);
                return;
            default:
                return;
        }
    }
}