using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class Aviator : ModItem
{
    public const float DAMAGE_RESISTANCE = 0.2f;

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(gold: 1);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WormScarf);
        recipe.AddIngredient(ItemID.CloudinaBottle);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.GetModPlayer<AirbornePlayer>().Airborne)
        {
            player.GetModPlayer<DamageResistancePlayer>().AddDR(
                DAMAGE_RESISTANCE
            );
        }
    }
}