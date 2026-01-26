using Terraria.Localization;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class HunterSynergy : HunterAccessory
{
    public const int CRIT_LEECH_PERCENT = 33;
    int critBonus = 0;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(critBonus, bleedChanceFormatArg);
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 50);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        Item maxCritWeapon = player.inventory.MaxBy(i => (i != null && i.stack > 0) ? i.crit : 0);
        if (maxCritWeapon != null)
        {
            critBonus = (int)(CRIT_LEECH_PERCENT * (maxCritWeapon.crit + 4) / 100f);
            player.GetModPlayer<CritPlayer>().summonCritChance += critBonus;
        }
    }
}
