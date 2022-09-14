using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class ArcaneGrace : ModItem
{
    public const int CHANCE = 40;
    public const float LIFE_REGEN = 0.01f;
    public const int BUFF_DURATION = 480;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"When damaged: {CHANCE}% chance for +{LIFE_REGEN * 100}% max life regen per second for {BUFF_DURATION / 60} seconds");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = -12;
        Item.expert = true;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 5);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ArcaneGracePlayer>().enabled = true;
    }
}
class ArcaneGracePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        if (Main.rand.Next(0, 100) < ArcaneGrace.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneGraceBuff>(), ArcaneGrace.BUFF_DURATION);
    }
    public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        => ApplyBuff();
    public bool Active => Player.HasBuff<ArcaneGraceBuff>();
    public float HealPerSecond => Player.statLifeMax2 * ArcaneGrace.LIFE_REGEN;
    const int HEAL_COOLDOWN = 60;
    int healTimer = 0;
    public override void UpdateLifeRegen()
    {
        if (Active)
        {
            healTimer++;
            if (healTimer > HEAL_COOLDOWN && Player.statLife < Player.statLifeMax2)
            {
                Player.Heal((int)MathF.Round(HealPerSecond));
                healTimer = 0;
            }
        }
    }
}