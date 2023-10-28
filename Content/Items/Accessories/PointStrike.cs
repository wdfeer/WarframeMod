using Terraria.Localization;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class PointStrike : ModItem
{
    public const int RELATIVE_CRIT_CHANCE_PERCENT = 60;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RELATIVE_CRIT_CHANCE_PERCENT);
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 66);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<CritPlayer>().relativeCritChance += RELATIVE_CRIT_CHANCE_PERCENT / 100f;
    }
}
