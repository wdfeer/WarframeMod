using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using WarframeMod.Content.Projectiles;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalItems;
using WarframeMod.Common.GlobalProjectiles;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;
public class Amprex : ModItem
{
    public const int ELECTRO_CHANCE = 22;
    public const int CRIT_DAMAGE_BONUS_PERCENT = 10;
    public const int MULTIHIT_DAMAGE_REDUCTION_PERCENT = 50;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ELECTRO_CHANCE, $"+{CRIT_DAMAGE_BONUS_PERCENT}%", MULTIHIT_DAMAGE_REDUCTION_PERCENT);
    public override void SetDefaults()
    {
        Item.damage = 26;
        Item.crit = 28;
        Item.DamageType = DamageClass.Magic;
        Item.width = 45;
        Item.height = 10;
        Item.useTime = 5;
        Item.useAnimation = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = Item.buyPrice(gold: 5);
        Item.rare = 7;
        // Item.UseSound = SoundID.Item93.WithVolumeScale(0.1f);
        Item.UseSound = SoundID.Item91.WithVolumeScale(0.4f);
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<AmprexProjectile>();
        Item.shootSpeed = 16f;
        Item.mana = 4;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TitaniumBar, 14);
        recipe.AddIngredient<Fieldron>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AdamantiteBar, 14);
        recipe.AddIngredient<Fieldron>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = WeaponCommon.ShootWith(this, player, source, position, velocity, type, damage, knockback, spawnOffset: 64);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier += CRIT_DAMAGE_BONUS_PERCENT / 100f;
        proj.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(ELECTRO_CHANCE / 100f);

        return false;
    }
}