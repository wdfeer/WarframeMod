using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class InfectedClip : ModItem
{
    public const int TOXIN_CHANCE = 10;

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 50);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BuffPlayer>().AddBuffChance(StackableBuff.Toxin, TOXIN_CHANCE);
    }
}