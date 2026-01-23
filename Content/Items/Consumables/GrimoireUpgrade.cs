using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Consumables;

public abstract class GrimoireUpgrade : ModItem
{
    public static string[] Upgrades = ["VomeInvocation"];
    public abstract uint Index { get; }
    public string Upgrade => Upgrades[Index];
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
        if (grimoire.upgrades.Contains(Upgrade))
            return false;

        grimoire.upgrades.Add(Upgrade);
        return true;
    }
}