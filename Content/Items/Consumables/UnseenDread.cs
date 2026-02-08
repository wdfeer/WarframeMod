using Terraria.Localization;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Consumables;

public class UnseenDread : ModItem
{
    public const int CRITICAL_DAMAGE_BONUS_PERCENT = 200;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRITICAL_DAMAGE_BONUS_PERCENT}%");

    public override void SetDefaults()
    {
        Item.rare = 6;
        Item.width = 44;
        Item.height = 64;
        Item.consumable = true;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item4;
        Item.value = Item.sellPrice(gold: 2);
    }

    public override bool? UseItem(Player player)
    {
        foreach (Item item in player.inventory)
        {
            if (item.ModItem is Dread { unseenDread: false } dread)
            {
                dread.unseenDread = true;
                return true;
            }
        }

        return false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HallowedBar, 4);
        recipe.AddIngredient(ItemID.SoulofNight, 15);
        recipe.AddIngredient(ItemID.InvisibilityPotion);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}