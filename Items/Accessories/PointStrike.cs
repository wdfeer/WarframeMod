using WarframeMod.Players;

namespace WarframeMod.Items.Accessories;

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
        Item.value = Terraria.Item.sellPrice(silver: 66);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<CritsPlayer>().relativeCritChance += 0.6f;
    }
}
