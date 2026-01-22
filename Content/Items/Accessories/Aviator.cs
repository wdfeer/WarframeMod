namespace WarframeMod.Content.Items.Accessories;

public class Aviator : ModItem
{
    public const float INCOMING_DAMAGE_MULT = 0.8f;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(gold: 1);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WormScarf, 1);
        recipe.AddIngredient(ItemID.CloudinaBottle, 1);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AviatorPlayer>().enabled = true;
    }
}
class AviatorPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
        if (!enabled)
            return;
        Player.UpdateTouchingTiles();
        bool touchingTiles = Player.TouchedTiles.Any();
        if (!touchingTiles)
            modifiers.SourceDamage *= Aviator.INCOMING_DAMAGE_MULT;
    }
}