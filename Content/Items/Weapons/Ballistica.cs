using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class Ballistica : ModItem
{
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
        Item.value = Item.buyPrice(gold: 5);
        Item.rare = 2;
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
        float chargeMult = (float)Math.Clamp(timeSinceLastShot / Item.useTime * 0.6f, 1, 2);
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