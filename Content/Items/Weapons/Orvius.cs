using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Common;
using WarframeMod.Content.Projectiles;
using WarframeMod.Content.Buffs;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;
public class Orvius : BaseGlaive
{
    public const int SLOW_CHANCE = 100;
    public const int BLEED_CHANCE = 20;
    public const float CRIT_MULT = 1.1f;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 69;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.crit = 16;
        Item.value = Item.sellPrice(gold: 2);
        Item.rare = ItemRarityID.LightRed;
        Item.shoot = ModContent.ProjectileType<OrviusProjectile>();
        Item.shootSpeed = 19f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AdamantiteBar, 12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TitaniumBar, 12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void OnShoot()
    {
        var buffProjectile = Proj.GetGlobalProjectile<BuffGlobalProjectile>();
        buffProjectile.AddBleed(BLEED_CHANCE / 100f);

        var critProjectile = Proj.GetGlobalProjectile<CritGlobalProjectile>();
        critProjectile.CritMultiplier = CRIT_MULT;
    }
    public override void PreExplode()
    {
        var buffProjectile = Proj.GetGlobalProjectile<BuffGlobalProjectile>();
        buffProjectile.AddBuff(new BuffChance(ModContent.BuffType<ColdDebuff>(), ColdDebuff.DEFAULT_DURATION, SLOW_CHANCE));
    }
}