using Terraria.Localization;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Consumables;

public class ShatteringJustice : ModItem
{
    public const int BASE_DAMAGE_INCREASE = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BASE_DAMAGE_INCREASE);

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 64;
        Item.consumable = true;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item4;
    }

    public override bool? UseItem(Player player)
    {
        foreach (Item item in player.inventory)
        {
            if (item.ModItem is Sobek { shatteringJustice: false } sobek)
            {
                sobek.shatteringJustice = true;
                return true;
            }
        }

        return false;
    }
}