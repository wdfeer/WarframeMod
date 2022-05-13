using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace WarframeMod.Items
{
    internal class Cernos : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots high velocity arrows with extra penetration");
        }
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.crit = 32;
            Item.knockBack = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 36;
            Item.height = 54;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.rare = 2;
            Item.value = 6000;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
        public override void AddRecipes()
        {
            int[] bowTypes = { ItemID.SilverBow, ItemID.TungstenBow, ItemID.LeadBow, ItemID.IronBow };
            for (int i = 0; i < bowTypes.Length; i++)
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(bowTypes[i]);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.Register();
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position += velocity.SafeNormalize(Vector2.Zero) * Item.width;
            int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            Projectile projectile = Main.projectile[projectileID];
            if (projectile.penetrate != -1) projectile.penetrate++;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.extraUpdates += 1;
            return false;
        }
    }
}