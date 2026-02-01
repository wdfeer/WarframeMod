namespace WarframeMod.Content.Items.Accessories;

public class MorphicTransformer : ModItem
{
    public const float DAMAGE_BONUS = 0.2f;

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 4;
        Item.value = Item.buyPrice(gold: 1);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AvengerEmblem);
        recipe.AddIngredient(ItemID.SoulofFlight);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.wingTime < player.wingTimeMax)
        {
            player.GetDamage(DamageClass.Generic) += DAMAGE_BONUS;
        }
    }
}