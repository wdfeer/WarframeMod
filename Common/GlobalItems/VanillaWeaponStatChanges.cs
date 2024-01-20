using WarframeMod.Common.Configs;

namespace WarframeMod.Common.GlobalItems;

internal class VanillaWeaponStatChanges : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        if (IsVanillaWeapon(item))
            item.crit += GetExtraCrit(item);
    }
    static bool IsVanillaWeapon(Item item)
        => item.ModItem == null && (item.damage > 0 || item.DamageType != DamageClass.Default || item.crit > 0) && item.ammo == AmmoID.None;
    static int GetExtraCrit(Item item)
    {
        if (item.ammo > 0)
            return 0;
        return GetConfigCritIncrease();
    }
    static int GetConfigCritIncrease()
        => ModContent.GetInstance<WarframeServerConfig>().vanillaCritIncrease;
}
