using WarframeMod.Common.GlobalItems;

namespace WarframeMod.Content.Items.Accessories;

public class Reach : ModItem
{
    public const int ABSOLUTE_RANGE_BONUS = 75;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{ABSOLUTE_RANGE_BONUS / 15} tiles of True Melee and Whip range\n+{WhipRange.GetWhipExtraRange(ABSOLUTE_RANGE_BONUS) * 100}% whip range");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 80);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<TrueMeleeRangePlayer>().absoluteExtraRange += ABSOLUTE_RANGE_BONUS;
    }
}
class TrueMeleeRangePlayer : ModPlayer
{
    public int absoluteExtraRange;
    public float rangeMult;
    public override void ResetEffects()
    {
        absoluteExtraRange = 0;
        rangeMult = 1f;
    }
}