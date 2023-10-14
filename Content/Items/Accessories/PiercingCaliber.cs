using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class PiercingCaliber : ModItem
{
    public const float CHANCE = 0.2f;
    public const int DURATION = 720;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 6;
        Item.value = Item.sellPrice(gold: 2);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<PiercingHit>());
        recipe.AddIngredient(ItemID.HallowedBar, 4);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BuffPlayer>().buffsOnHitNPC.Add(new BuffChance(BuffID.Weak, DURATION, CHANCE));
    }
}