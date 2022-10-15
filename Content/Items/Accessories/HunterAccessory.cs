using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public abstract class HunterAccessory : ModItem
{
    public abstract string DefaultTooltip { get; }
    public const float MINION_BLEED_CHANCE_PERCENT = 7.5f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault(DefaultTooltip + $"\n+{MINION_BLEED_CHANCE_PERCENT}% summon bleeding chance");
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BuffPlayer>().AddBleedChance(DamageClass.Summon, MINION_BLEED_CHANCE_PERCENT / 100f);
    }
}
