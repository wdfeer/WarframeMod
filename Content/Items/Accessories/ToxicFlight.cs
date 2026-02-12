using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class ToxicFlight : ModItem
{
    public const int MOVE_SPEED = 12;
    public const int WING_TIME_SECONDS = 1;
    public const int TOXIN_CHANCE = 12;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MOVE_SPEED, WING_TIME_SECONDS, TOXIN_CHANCE);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 9;
        Item.value = Item.sellPrice(gold: 15);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.moveSpeed += MOVE_SPEED / 100f;
        player.wingTimeMax += WING_TIME_SECONDS * 60;
        player.GetModPlayer<BuffPlayer>().AddBuffChance(StackableBuff.Toxin, TOXIN_CHANCE);
    }
}