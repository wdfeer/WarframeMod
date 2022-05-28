using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria.DataStructures;
using WarframeMod.Projectiles;

namespace WarframeMod.Items
{
    public class Nukor : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+100% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.crit = -2;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.width = 32;
            Item.height = 24;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(silver: 45);
            Item.rare = 3;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.NukorProjectile>();
            Item.shootSpeed = 12f;
            Item.mana = 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity , type, damage, knockback, player.whoAmI);
            NukorProjectile modProjectile = projectile.ModProjectile as NukorProjectile;
            modProjectile.iFrames = Item.useTime;
            return false;
        }
    }
}