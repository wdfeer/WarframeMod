namespace WarframeMod.Content.Items.Accessories;

public class Vitality : ModItem
{
    public const int EXTRA_MAX_LIFE = 60;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 1;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.sellPrice(silver: 20);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statLifeMax2 += EXTRA_MAX_LIFE;
    }
}