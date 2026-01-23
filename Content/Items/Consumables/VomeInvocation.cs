using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Consumables;

public class VomeInvocation : GrimoireUpgrade
{
    public override GrimoireUpgradeType UpgradeType => GrimoireUpgradeType.VomeInvocation;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 2);
    }
}

class VomeInvocationPlayer : ModPlayer
{
    public int stacks;

    public override void ResetEffects()
    {
        if (!Player.HasBuff<VomeInvocationBuff>())
            stacks = 0;
        else if (stacks > 15)
            stacks = 15;
    }

    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.upgrades.Contains(GrimoireUpgradeType.VomeInvocation))
        {
            damage.Base += 20;
            damage += 0.04f * stacks;
        }
    }
}