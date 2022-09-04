using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod.Content.Items.Accessories;
public class HyperionThrusters : ModItem
{
    public const float VERTICAL_WING_SPEED_BONUS = 0.3f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{(int)(VERTICAL_WING_SPEED_BONUS * 100f)}% Wing vertical speed");
    }

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 4;
        Item.value = Item.buyPrice(gold: 1);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.OrichalcumBar, 5);
        recipe.AddIngredient(ItemID.Feather, 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MythrilBar, 5);
        recipe.AddIngredient(ItemID.Feather, 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<VerticalWingSpeedPlayer>().verticalWingSpeedMult += VERTICAL_WING_SPEED_BONUS;
    }
}
class VerticalWingSpeedPlayer : ModPlayer
{
    public float verticalWingSpeedMult = 1f;
    public override void ResetEffects()
        => verticalWingSpeedMult = 1f;
}
class VerticalWingSpeedGlobalItem : GlobalItem
{
    public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        maxAscentMultiplier *= player.GetModPlayer<VerticalWingSpeedPlayer>().verticalWingSpeedMult;
    }
}