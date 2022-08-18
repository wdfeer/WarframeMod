using Terraria.ID;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class Bite : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+12% minion Critical Chance and +8% minion Critical Damage");
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
        var critPlayer = player.GetModPlayer<CritsPlayer>();
        critPlayer.summonCritChance += 12;
        critPlayer.summonCritMult += 0.08f;
    }
}
