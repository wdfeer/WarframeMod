using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace WarframeMod.Items
{
    public class Redeemer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires 6 pellets without consuming ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.crit = 10;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 24;
            Item.scale = 1f;
            Item.useTime = 72;
            Item.useAnimation = 72;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = 15000;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 12f;
            Item.UseSound = SoundID.Item36.WithVolume(0.9f).WithPitchVariance(-0.1f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LeadBar, 11);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 11);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position += velocity.SafeNormalize(Vector2.Zero) * Item.width;
            for (int i = 0; i < 6; i++)
            {
                int projectileID = Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(15)), type, damage, knockback, player.whoAmI);
                var projectile = Main.projectile[projectileID];
                projectile.DamageType = DamageClass.Melee;
            }

            return false;
        }
    }
}