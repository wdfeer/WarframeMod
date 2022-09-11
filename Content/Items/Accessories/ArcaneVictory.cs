using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class ArcaneVictory : ModItem
{
    public const int CHANCE = 20;
    public const float LIFE_REGEN = 0.005f;
    public const int BUFF_DURATION = 600;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On Critical hit: {CHANCE}% chance for +{LIFE_REGEN * 100}% max life regen per second for {BUFF_DURATION / 60} seconds");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = -12;
        Item.expert = true;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 4);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ArcaneVictoryPlayer>().enabled = true;
    }
}
class ArcaneVictoryPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        if (Main.rand.Next(0, 100) < ArcaneVictory.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneVictoryBuff>(), ArcaneVictory.BUFF_DURATION);
    }
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        if (crit) ApplyBuff();
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (crit) ApplyBuff();
    }
    public bool Active => Player.HasBuff<ArcaneVictoryBuff>();
    public float HealPerSecond => Player.statLifeMax2 * ArcaneVictory.LIFE_REGEN;
    const int healCooldown = 60;
    int healTimer = 0;
    public override void UpdateLifeRegen()
    {
        if (Active)
        {
            healTimer++;
            if (healTimer > healCooldown && Player.statLife < Player.statLifeMax2)
            {
                Player.Heal((int)HealPerSecond);
                healTimer = 0;
            }
        }
    }
}