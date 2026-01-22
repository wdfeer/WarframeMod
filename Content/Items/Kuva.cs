namespace WarframeMod.Content.Items;
public class Kuva : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(silver: 20);
        Item.maxStack = 99;
        Item.width = 32;
        Item.height = 32;
    }
}