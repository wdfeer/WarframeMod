using Terraria.Audio;
using Terraria.DataStructures;

namespace WarframeMod.Items.Weapons;

internal class Sybaris : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots a 2-round burst");
    }
    public override void SetDefaults()
    {
        Item.damage = 17;
        Item.crit = 21;
        Item.knockBack = 3;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 46;
        Item.height = 14;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 5;
        Item.useAnimation = 10;
        Item.reuseDelay = 30;
        Item.rare = 2;
        Item.value = Item.buyPrice(gold: 1, silver: 50);
        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(0, -0.5f);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Musket);
        recipe.AddIngredient(ItemID.FlintlockPistol);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item11, position);

        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Projectile projectile = Main.projectile[projectileID];
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = -1;
        return false;
    }
}