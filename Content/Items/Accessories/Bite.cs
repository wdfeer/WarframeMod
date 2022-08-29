using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class Bite : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+12% minion Critical Chance and +10% minion Critical Damage");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(gold: 2);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var critPlayer = player.GetModPlayer<CritPlayer>();
        critPlayer.summonCritChance += 12;
        critPlayer.summonCritMult += 0.1f;
    }
}
