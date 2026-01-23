using Terraria.Localization;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Consumables;

public class XataInvocation : GrimoireUpgrade
{
    public override GrimoireUpgradeType UpgradeType => GrimoireUpgradeType.XataInvocation;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = 8;
        Item.value = Item.sellPrice(gold: 4);
    }
}

class XataInvocationPlayer : ModPlayer
{
    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.XataInvocation))
            damage.Base += 50;
    }
    public override void ModifyWeaponCrit(Item item, ref float crit)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.XataInvocation))
            crit += 20;
    }
}