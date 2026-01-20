namespace WarframeMod.Content.Items.Accessories;

public class UmbralFiber : UmbralAccessory
{
    public readonly int[] DEFENSE = [7, 10, 13];
    public override string UniqueTooltipDefault => $"+{DEFENSE[0]} defense";
    public override string GetCurrentUniqueTooltipValue(int umbraCount)
        => $"+{DEFENSE[umbraCount]} defense";
    public override void UpdateUmbralAccessory(Player player, int umbraCount)
    {
        player.statDefense += DEFENSE[umbraCount];
    }
    public override int NonUmbralItemType => ModContent.ItemType<SteelFiber>();
}