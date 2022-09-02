using WarframeMod.Common.Players;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class VitalSense : ModItem
{
    public const float EXTRA_CRIT_MULT = 0.2f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{(int)(EXTRA_CRIT_MULT * 100f)}% Critical Damage");
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.accessory = true;
        Item.rare = 4;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 6);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<CritPlayer>().critMultiplierPlayer += EXTRA_CRIT_MULT;
    }
}