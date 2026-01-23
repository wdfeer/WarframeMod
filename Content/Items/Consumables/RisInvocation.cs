using Terraria.Localization;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Consumables;

public class RisInvocation : GrimoireUpgrade
{
    public override GrimoireUpgradeType UpgradeType => GrimoireUpgradeType.RisInvocation;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = 9;
        Item.value = Item.sellPrice(gold: 8);
    }
}

class RisInvocationPlayer : ModPlayer
{
    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.RisInvocation))
            damage.Base += 60;
    }
    public override float UseSpeedMultiplier(Item item)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.RisInvocation) && Player.altFunctionUse != 2)
            return 1.5f;
        return 1f;
    }
    public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type,
        ref int damage,
        ref float knockback)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.RisInvocation) &&
            type == ModContent.ProjectileType<GrimoireAltProjectile>())
        {
            velocity *= 2;
        }
    }
}
