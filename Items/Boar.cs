using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod.Items
{
	public class Boar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A really inaccurate automatic shotgun, shoots 4 pellets at once");
		}

		public override void SetDefaults()
		{
			Item.damage = 3;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 17;
			Item.scale = 1.2f;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 1;
			Item.value = Item.buyPrice(silver: 60);
			Item.rare = 2;
			Item.UseSound = SoundID.Item36.WithVolume(0.6f).WithPitchVariance(-0.2f);
			Item.ammo = AmmoID.Bullet;
			Item.shoot = ProjectileID.Bullet;
			Item.shootSpeed = 12;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LeadBar, 17);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IronBar, 17);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 4; i++)
            {
				position += velocity.SafeNormalize(Vector2.Zero) * Item.width;
				velocity = velocity.RotatedByRandom(MathHelper.ToRadians(16 * i));
				int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
				Projectile proj = Main.projectile[projectileID];
			}

			return false;
        }
    }
}