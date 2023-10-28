using Terraria.Localization;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class CriticalDelay : ModItem
{
    public const int RELATIVE_CRIT_PERCENT = 80;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RELATIVE_CRIT_PERCENT);
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 2);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<CritPlayer>().relativeCritChance += RELATIVE_CRIT_PERCENT / 100f;
        player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
    }
}
