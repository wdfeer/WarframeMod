using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class PointStrike : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+60% Crit Chance relative to the weapon's base crit chance");
    }
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
        player.GetModPlayer<CritPlayer>().relativeCritChance += 0.6f;
    }
}
