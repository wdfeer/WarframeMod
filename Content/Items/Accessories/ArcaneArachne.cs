using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class ArcaneArachne : ModItem
{
    public const int DAMAGE_BUFF = 25;
    public const int COOLDOWN_DURATION = 1800;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{DAMAGE_BUFF}% Damage\nWhen damaged: disable the above effect for {COOLDOWN_DURATION / 60} seconds");
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.accessory = true;
        Item.rare = -12;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 4);
    }
    public override void UpdateInventory(Player player)
    {
        Item.rare = -12;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ArachnePlayer>().enabled = true;
        player.GetDamage(DamageClass.Generic) += DAMAGE_BUFF / 100f;
    }
}
class ArachnePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
    {
        if (enabled)
            Player.AddBuff(ModContent.BuffType<ArcaneArachneBuff>(), ArcaneArachne.COOLDOWN_DURATION);
    }
}