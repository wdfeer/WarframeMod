using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class ArcaneStrike : ModItem
{
    public const int CHANCE = 20;
    public const int SPEED_BUFF = 16;
    public const int BUFF_DURATION = 600;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On hit: {CHANCE}% chance for +{SPEED_BUFF}% Melee Speed for {BUFF_DURATION / 60} seconds");
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.accessory = true;
        Item.rare = -12;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 3);
    }
    public override void UpdateInventory(Player player)
    {
        Item.rare = -12;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ArcaneStrikePlayer>().enabled = true;
    }
}
class ArcaneStrikePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public int currentDefense = 0;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        if (Main.rand.Next(0, 100) < ArcaneStrike.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneStrikeBuff>(), ArcaneStrike.BUFF_DURATION);
    }
    public override void OnHitAnything(float x, float y, Entity victim)
    {
        ApplyBuff();
    }
}