namespace WarframeMod.Content.Items;
public class Fieldron : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.buyPrice(gold: 3);
        Item.maxStack = 99;
        Item.width = 32;
        Item.height = 32;
    }
}