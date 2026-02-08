using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;

public class DotdTonkor : KuvaTonkor
{
    public const int CRIT_DAMAGE_INCREASE_PERCENT = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRIT_DAMAGE_INCREASE_PERCENT}%");

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 97;
        Item.crit = 35;
        Item.knockBack /= 2;
        Item.width = 37;
        Item.height = 14;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.sellPrice(gold: 9);
    }

    public override void AddRecipes()
    {
    }
}