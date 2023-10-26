using WarframeMod.Common.Configs;

namespace WarframeMod.Common.GlobalItems;

internal class VanillaWeaponStatChanges : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        if (item.ModItem != null)
            return;
        item.crit += GetExtraCrit(item);
    }
    int GetExtraCrit(Item item)
    {
        if (item.ammo > 0)
            return 0;
        return GetConfigCritIncrease();
    }
    static int GetConfigCritIncrease()
        => ModContent.GetInstance<WarframeServerConfig>().vanillaCritIncrease;
}
