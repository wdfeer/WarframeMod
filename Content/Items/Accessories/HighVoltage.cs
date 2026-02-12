using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class HighVoltage : ModItem
{
    public const int ELECTRO_CHANCE_PERCENT = 12;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        BuffPlayer buffman = player.GetModPlayer<BuffPlayer>();
        buffman.stackableBuffsOnHitNPC.Add(new StackableBuffChance(StackableBuff.Electricity, ELECTRO_CHANCE_PERCENT));
    }
}
