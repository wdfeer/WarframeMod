using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Consumables;

public abstract class GrimoireUpgrade : ModItem
{
    public abstract GrimoireUpgradeType UpgradeType { get; }
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 64;
        Item.consumable = true;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item4;
    }
    public override bool? UseItem(Player player)
    {
        var grimoire = Grimoire.GetPlayerGrimoire(player);
        if (grimoire.upgrades.Contains(UpgradeType))
            return false;

        grimoire.upgrades.Add(UpgradeType);
        return true;
    }
}
public enum GrimoireUpgradeType : uint
{
    VomeInvocation,
    LohkCanticle,
    RisInvocation
}