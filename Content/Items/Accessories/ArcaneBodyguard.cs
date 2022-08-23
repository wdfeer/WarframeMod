using Terraria.DataStructures;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class ArcaneBodyguard : ModItem
{
    public const int CHANCE = 10;
    public const int DAMAGE_REDUCTION = 10;
    public const int BUFF_DURATION = 720;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On summon hit: {CHANCE}% chance for +{DAMAGE_REDUCTION}% Damage Reduction for {BUFF_DURATION / 60} seconds");
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
        player.GetModPlayer<BodyguardPlayer>().enabled = true;
    }
}
class BodyguardPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        Player.AddBuff(ModContent.BuffType<ArcaneBodyguardBuff>(), ArcaneBodyguard.BUFF_DURATION);
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (proj.DamageType == DamageClass.Summon || proj.DamageType == DamageClass.SummonMeleeSpeed)
            ApplyBuff();
    }
    bool Active => Player.HasBuff(ModContent.BuffType<ArcaneBodyguardBuff>());
    void ModifyIncomingDamage(ref int damage)
    {
        float damageMult = (100 - ArcaneBodyguard.DAMAGE_REDUCTION) / 100f;
        damage = (int)(damage * damageMult);
    }
    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        if (Active)
            ModifyIncomingDamage(ref damage);
        return true;
    }
}