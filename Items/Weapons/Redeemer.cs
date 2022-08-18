using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Projectiles;

namespace WarframeMod.Items.Weapons;

public class Redeemer : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Fires 6 pellets without consuming ammo\nHas linear damage falloff");
    }
    public override void SetDefaults()
    {
        Item.damage = 22;
        Item.crit = 10;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
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
        Item.UseSound = WeaponCommon.ModifySoundStyle(new SoundStyle("WarframeMod/Sounds/RedeemerPrimeSound"), 0.8f, 0.06f);
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
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        for (int i = 0; i < 6; i++)
        {
            int projectileID = Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(15)), ModContent.ProjectileType<RedeemerBullet>(), damage, knockback, player.whoAmI);
            var projectile = Main.projectile[projectileID];
            projectile.DamageType = DamageClass.Melee;
        }

        return false;
    }
}