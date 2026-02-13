using Terraria.Localization;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class MotusImpact : MotusAccessory
{
    public const int RANGE_BONUS_TILES = 6;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RANGE_BONUS_TILES, KNOCKBACK_REDUCTION);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 80);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        if (player.GetModPlayer<AirbornePlayer>().Airborne)
            player.GetModPlayer<TrueMeleeRangePlayer>().absoluteExtraRange += RANGE_BONUS_TILES * 16;
    }
}