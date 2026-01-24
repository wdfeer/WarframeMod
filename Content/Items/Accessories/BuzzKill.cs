using Terraria.Localization;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class BuzzKill : ModItem
{
    public const int BLEED_CHANCE_PERCENT = 15;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE_PERCENT);
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 4);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        BuffPlayer buffman = player.GetModPlayer<BuffPlayer>();
        buffman.AddBleedChance(DamageClass.Melee, BLEED_CHANCE_PERCENT / 100f);
    }
}
