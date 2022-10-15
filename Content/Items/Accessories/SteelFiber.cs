namespace WarframeMod.Content.Items.Accessories;

public class SteelFiber : ModItem
{
    public const int DEFENSE = 7;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{DEFENSE} defense");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 2;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(silver: 80);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statDefense += DEFENSE;
    }
}