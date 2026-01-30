namespace WarframeMod.Content.Items;
public class Fieldron : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(gold: 8);
        Item.maxStack = 99;
        Item.width = 32;
        Item.height = 32;
    }
}