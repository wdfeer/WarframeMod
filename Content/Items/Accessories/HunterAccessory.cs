using Terraria.Localization;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public abstract class HunterAccessory : ModItem
{
    public const float MINION_BLEED_CHANCE_PERCENT = 7.5f;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MINION_BLEED_CHANCE_PERCENT.ToString("F1"));

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BuffPlayer>().AddBleedChance(DamageClass.Summon, MINION_BLEED_CHANCE_PERCENT / 100f);
    }
}