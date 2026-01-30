using Terraria.Localization;

namespace WarframeMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class CrewmanHelmet : ModItem
{
    public const int CRIT_CHANCE = 3;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CRIT_CHANCE);

    public override void SetStaticDefaults()
    {
        ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 50);
        Item.rare = ItemRarityID.Blue;
        Item.defense = 2;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 4)
            .AddIngredient(ItemID.SilverCoin, 50)
            .AddTile(TileID.Anvils)
            .Register();
    }

    public override void UpdateEquip(Player player)
    {
        player.GetCritChance<GenericDamageClass>() += 3;
    }
}