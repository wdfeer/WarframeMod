using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria.DataStructures;
using WarframeMod.Projectiles;
using WarframeMod.Global;

namespace WarframeMod.Items.Weapons
{
    public class Synapse : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+35% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.crit = 31;
            Item.mana = 3;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.width = 48;
            Item.height = 11;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.buyPrice(gold: 1, silver: 20);
            Item.rare = 4;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SynapseProjectile>();
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.Ichor, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            projectile.GetGlobalProjectile<WarframeGlobalProjectile>().CritMultiplier = 1.35f;
            return false;
        }
    }
}