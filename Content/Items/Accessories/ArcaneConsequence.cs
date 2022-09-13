using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class ArcaneConsequence : ModItem
{
    public const int PERCENT_MOVEMENT_SPEED_INCREASE = 20;
    public const int PERCENT_WING_SPEED_INCREASE = 12;
    public const int DURATION = 360;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On Critical hit: +{PERCENT_MOVEMENT_SPEED_INCREASE}% movement speed and +{PERCENT_WING_SPEED_INCREASE} wing flight speed for {DURATION / 60} seconds");
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.accessory = true;
        Item.rare = -12;
        Item.expert = true;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 3);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ConsequencePlayer>().enabled = true;
    }
}
class ConsequencePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    void OnHit(bool crit)
    {
        if (enabled && crit)
            Player.AddBuff(ModContent.BuffType<ArcaneConsequenceBuff>(), ArcaneConsequence.DURATION);
    }
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        => OnHit(crit);
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        => OnHit(crit);
    public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        => OnHit(crit);
    public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
        => OnHit(crit);
}