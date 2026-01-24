using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Consumables;

public class JahuCanticle : GrimoireUpgrade
{
    public const int ICHOR_DISTANCE = 50 * 16;
    public override GrimoireUpgradeType UpgradeType => GrimoireUpgradeType.JahuCanticle;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 2);
    }
}

class JahuCanticlePlayer : ModPlayer
{
    public override float UseSpeedMultiplier(Item item)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.JahuCanticle)) return 1.15f;
        return 1f;
    }
}