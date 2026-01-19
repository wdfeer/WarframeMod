using WarframeMod.Common.Players;

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
        recipe.AddIngredient(ItemID.AvengerEmblem, 1);
        recipe.AddIngredient(ItemID.SoulofFlight, 1);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.GetModPlayer<MorphicTransformerPlayer>().winging)
        {
            player.GetDamage(DamageClass.Generic) += DAMAGE_BONUS;
        }
    }
}

class MorphicTransformerPlayer : ModPlayer
{
    public bool winging = false;
}

class MorphicTransformerWings : GlobalItem
{
    public override bool WingUpdate(int wings, Player player, bool inUse)
    {
        player.GetModPlayer<MorphicTransformerPlayer>().winging = inUse;
        return base.WingUpdate(wings, player, inUse);
    }

}