using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneBlessing : Arcane
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MAX_HP_PER_LIFE, MAX_STACKS);
    public const int MAX_HP_PER_LIFE = 5;
    public const int MAX_STACKS = 24;
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        Player player = Main.LocalPlayer;
        ArcaneBlessingPlayer blessedPlayer = player.GetModPlayer<ArcaneBlessingPlayer>();
        if (blessedPlayer.enabled)
        {
            int expertIndex = tooltips.FindIndex(tip => tip.Text == "Expert");
            if (expertIndex == -1)
                return;
            TooltipLine line = new(Mod, "ActiveBonus", $"Current bonus: +{blessedPlayer.CurrentBonus}");
            tooltips.Insert(expertIndex, line);
        }
    }
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneBlessingPlayer>().enabled = true;
    }
}
class ArcaneBlessingPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public int CurrentBonus => MaxHpPerStack * stacks;
    public static int MaxHpPerStack => ArcaneBlessing.MAX_HP_PER_LIFE;
    public int stacks = 0;
    public override void PostUpdateEquips()
    {
        if (!enabled)
            stacks = 0;
        else
        {
            Player.statLifeMax2 += CurrentBonus;
        }
    }
    public override void UpdateDead()
    {
        stacks = 0;
    }
    public void OnPickupHeartWhenEnabled()
    {
        if (stacks < ArcaneBlessing.MAX_STACKS)
            stacks++;
    }
    public readonly int[] heartTypes = [ItemID.Heart, ItemID.CandyApple, ItemID.CandyCane];
    public override bool OnPickup(Item item)
    {
        if (enabled && heartTypes.Contains(item.type))
            OnPickupHeartWhenEnabled();
        return base.OnPickup(item);
    }
}