using Terraria.DataStructures;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class EternalOnslaught : ModItem
{
    public const int CRIT_CHANCE_BONUS = 25;
    public const int DURATION = 360;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"When a mana potion is used: +{CRIT_CHANCE_BONUS}% magic crit chance for {DURATION / 60} seconds");
    }

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = -12;
        Item.expert = true;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 3);
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