using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class AnabolicPollination : ModItem
{
    public const int TOXIN_CHANCE_PER_MINION_SLOT = 3;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(TOXIN_CHANCE_PER_MINION_SLOT);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.buyPrice(gold: 30);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var mult = player.maxMinions;
        player.GetModPlayer<BuffPlayer>().AddBuffChance(StackableBuff.Toxin, TOXIN_CHANCE_PER_MINION_SLOT * mult);
    }
}