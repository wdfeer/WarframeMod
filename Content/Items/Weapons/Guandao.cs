using Terraria.Localization;
using WarframeMod.Common.GlobalItems;

namespace WarframeMod.Content.Items.Weapons;

internal class Guandao : CircularMelee
{
    public const int CRIT_DAMAGE_BONUS_PERCENT = 10;
    public const int ENEMY_DEFENSE_LESS_THAN = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRIT_DAMAGE_BONUS_PERCENT}%", ENEMY_DEFENSE_LESS_THAN);
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 140;
        Item.crit = 24;
        Item.knockBack = 6f;
        Item.width = 100;
        Item.height = 65;
        Item.scale = 3f;
        Item.useTime = 50;
        Item.useAnimation = 50;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(gold: 27);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 1f + CRIT_DAMAGE_BONUS_PERCENT / 100f;
    }
    public override float HitboxSizeMult => 1.4f;
    public override Vector2 HitboxOffset => new(16, 0);
    public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
    {
        int defense = target.defense;
        if (target.HasBuff(BuffID.Ichor))
            defense -= 20; // target.defense is for some reason not decreased by default
        if (defense < ENEMY_DEFENSE_LESS_THAN)
        {
            modifiers.SourceDamage *= 2;
            modifiers.Knockback *= 2;
        }
    }
}