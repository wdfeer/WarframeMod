using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class EternalOnslaught : Arcane
{
    public const int CRIT_CHANCE_BONUS = 25;
    public const int DURATION = 360;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"When a mana potion is used: +{CRIT_CHANCE_BONUS}% magic crit chance for {DURATION / 60} seconds");
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<EternalOnslaughtPlayer>().enabled = true;
    }
}
class EternalOnslaughtPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
    {
        if (enabled)
            Player.AddBuff(ModContent.BuffType<EternalOnslaughtBuff>(), EternalOnslaught.DURATION);
    }
}