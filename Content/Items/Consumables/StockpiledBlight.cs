using Terraria.Localization;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Consumables;

public class StockpiledBlight : ModItem
{
    public const int BASE_DAMAGE_FLAT = 20;
    public const int FIRE_RATE_BONUS = 100;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BASE_DAMAGE_FLAT, FIRE_RATE_BONUS);

    public override void SetDefaults()
    {
        Item.rare = 5;
        Item.width = 44;
        Item.height = 64;
        Item.consumable = true;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.value = Item.sellPrice(gold: 6);
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item4;
    }

    public override bool? UseItem(Player player)
    {
        foreach (Item item in player.inventory)
        {
            if (item.ModItem is Kunai { stockpiledBlight: false } kunai)
            {
                kunai.stockpiledBlight = true;
                kunai.UpdateUpgradeStats();
                return true;
            }
        }

        return false;
    }
}