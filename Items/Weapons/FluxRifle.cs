using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace WarframeMod.Items.Weapons
{
    public class FluxRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Doesn't consume ammo\n10 Defense penetration");
        }
        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 45;
            Item.height = 16;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = 3;
            Item.UseSound = WeaponCommon.ModifySoundStyle(SoundID.Item11, 0.32f, 0.6f);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.FluxRifleProj>();
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBar, 9);
            recipe.AddIngredient(ItemID.TissueSample, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 9);
            recipe.AddIngredient(ItemID.TissueSample, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBar, 9);
            recipe.AddIngredient(ItemID.ShadowScale, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 9);
            recipe.AddIngredient(ItemID.ShadowScale, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}