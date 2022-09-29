namespace WarframeMod.Common.GlobalItems;

internal class VanillaWeaponStatChanges : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        if (item.ModItem != null)
            return;
        (int, int) extraDmgAndCrit = GetExtraDamageAndCrit(item);
        item.damage += extraDmgAndCrit.Item1;
        item.crit += extraDmgAndCrit.Item2;
    }
    (int, int) GetExtraDamageAndCrit(Item item)
    {
        if (item.ammo > 0)
            goto no;
        if (item.DamageType == DamageClass.MeleeNoSpeed)
            return ((int)(item.damage * -0.07f), 8);
        else if (item.DamageType == DamageClass.Melee)
            return ((int)(item.damage * -0.08f), 10);
        else if (item.DamageType == DamageClass.Ranged)
            return ((int)(item.damage * -0.06f), 6);
        else if (item.DamageType == DamageClass.Magic)
            return ((int)(item.damage * -0.07f), 7);
        no:
        return (0, 0);
    }
}
