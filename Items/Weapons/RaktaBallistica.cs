using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace WarframeMod.Items.Weapons
{
    public class RaktaBallistica : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots 4 arrows at once\nNot shooting charges the next shot, increasing damage and accuracy");
        }
        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 32;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = 3;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 16;
            Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo Item that this weapon uses. Note that this is not an Item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Ballistica>());
            recipe.AddIngredient(ItemID.TissueSample, 17);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        double lastShotTime = 0;
        double timeSinceLastShot = 60;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
            timeSinceLastShot = Main.time - lastShotTime;
            lastShotTime = Main.time;
            float chargeMult = (float)(timeSinceLastShot / Item.useTime);
            if (chargeMult < 1)
                chargeMult = 1;
            else if (chargeMult > 2)
                chargeMult = 2;
            for (int i = 0; i < 4; i++)
            {
                int projectileID = Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(7) / chargeMult), type, (int)(damage * chargeMult), knockback, player.whoAmI);
                Projectile projectile = Main.projectile[projectileID];
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = -1;
            }
            return false;
        }
    }
}