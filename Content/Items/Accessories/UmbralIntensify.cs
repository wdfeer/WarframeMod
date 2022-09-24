using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class UmbralIntensify : UmbralAccessory
{
    public readonly int[] PERCENT_DAMAGE = new int[] {12, 15, 18};
    public override string UniqueTooltipDefault => $"+{PERCENT_DAMAGE[0]}% damage";
    public override string GetCurrentUniqueTooltipValue(int umbraCount)
        => $"+{PERCENT_DAMAGE[umbraCount]}% damage";
    public override void UpdateUmbralAccessory(Player player, int umbraCount)
    {
        player.GetDamage(DamageClass.Generic) += PERCENT_DAMAGE[umbraCount] / 100f;
    }
    public override int NonUmbralItemType => ModContent.ItemType<Intensify>();
}