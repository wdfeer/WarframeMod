namespace WarframeMod.Content.Items.Accessories;

public class UmbralVitality : UmbralAccessory
{
    public readonly int[] EXTRA_MAX_LIFE = new int[] { 60, 80, 100 };
    public override string UniqueTooltipDefault => $"+{EXTRA_MAX_LIFE[0]} max life";
    public override string GetCurrentUniqueTooltipValue(int umbraCount)
        => $"+{EXTRA_MAX_LIFE[umbraCount]} max life";
    public override void UpdateUmbralAccessory(Player player, int umbraCount)
    {
        player.statLifeMax2 += EXTRA_MAX_LIFE[umbraCount];
    }
    public override int NonUmbralItemType => ModContent.ItemType<Vitality>();
}