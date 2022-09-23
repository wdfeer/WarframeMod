using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Gorgon : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Takes a while to spool up, shoots rapidly but inaccurately\n-25% Critical Damage");
    }
    public override void SetDefaults()
    {
        Item.damage = 6;
        Item.crit = 13;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 50;
        Item.height = 19;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1;
        Item.value = Item.buyPrice(gold: 3);
        Item.rare = 3;
        Item.UseSound = SoundID.Item11.ModifySoundStyle(0.75f);
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 12f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
        recipe.AddIngredient(ItemID.JungleSpores, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    double lastShotTime = 0;
    double timeSinceLastShot = 60;
    void UpdateTimeSinceLastShotAndLastShotTime()
    {
        timeSinceLastShot = Main.time - lastShotTime;
        lastShotTime = Main.time;
    }
    public override bool CanUseItem(Player player)
    {
        UpdateTimeSinceLastShotAndLastShotTime();
        if (Item.useTime > 6)
        {
            Item.useTime -= 2;
            Item.useAnimation -= 2;
            if (Item.useTime < 6)
            {
                Item.useTime = 6;
                Item.useAnimation = 6;
            }
        }
        else if (timeSinceLastShot > 14)
        {
            Item.useTime += (int)(timeSinceLastShot / 3);
            Item.useAnimation += (int)(timeSinceLastShot / 3);
            if (Item.useTime > 22)
            {
                Item.useTime = 22;
                Item.useAnimation = 22;
            }
        }

        return base.CanUseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        Projectile.NewProjectileDirect(source,
                                       position,
                                       velocity.RotatedByRandom(1f / timeSinceLastShot),
                                       type,
                                       damage,
                                       knockback,
                                       player.whoAmI)
                  .GetGlobalProjectile<CritGlobalProjectile>()
                  .CritMultiplier = 0.75f;
        return false;
    }
}