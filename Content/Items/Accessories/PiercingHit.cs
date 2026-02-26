using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class PiercingHit : ModItem
{
    public const int CHANCE = 12;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 20);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BuffPlayer>().AddBuffChance(StackableBuff.Weak, CHANCE);
    }
}