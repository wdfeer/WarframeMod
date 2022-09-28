using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class Ballistica : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots 4 arrows at once with greatly decreased velocity\nNot shooting charges the next shot, increasing damage, accuraccy and velocity\n-90% ammo damage");
    }
    public override void SetDefaults()
    {
        Item.damage = 7;
        Item.crit = 11;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 30;
        Item.height = 31;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1;
        Item.value = Item.sellPrice(silver: 60);
        Item.rare = 1;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = false;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 4;
        Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo Item that this weapon uses. Note that this is not an Item Id, but just a magic value.
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
        recipe.AddIngredient(ItemID.Silk, 7);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
    double lastShotTime = 0;
    double timeSinceLastShot = 60;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, 0.1f);
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        timeSinceLastShot = Main.time - lastShotTime;
        lastShotTime = Main.time;
        float chargeMult = (float)timeSinceLastShot / Item.useTime * 0.6f;
        if (chargeMult < 1)
            chargeMult = 1;
        else if (chargeMult > 2)
            chargeMult = 2;
        velocity *= chargeMult;
        for (int i = 0; i < 4; i++)
        {
            int projectileID = Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(12) / chargeMult), type, (int)(damage * chargeMult), knockback, player.whoAmI);
            Projectile projectile = Main.projectile[projectileID];
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        return false;
    }
}