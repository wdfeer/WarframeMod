using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class VirtuosStrike : ModItem
{
    public const int CHANCE = 15;
    public const float EXTRA_CRIT_MULT = 0.33f;
    public const int BUFF_DURATION = 420;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On magic Critical hit: {CHANCE}% chance for +{(int)(EXTRA_CRIT_MULT * 100)}% Critical Damage for {BUFF_DURATION / 60} seconds");
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
        player.GetModPlayer<VirtuosStrikePlayer>().enabled = true;
    }
}
class VirtuosStrikePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (enabled && proj.DamageType == DamageClass.Magic && Main.rand.Next(0,100) < VirtuosStrike.CHANCE)
            Player.AddBuff(ModContent.BuffType<VirtuosStrikeBuff>(), VirtuosStrike.BUFF_DURATION);
    }
}