using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Items.Weapons;

public class KuvaKohm : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($@"Takes a while to spool up while increasing Multishot up to {maxMultishot} pellets
33% chance to apply bleeding on hit
Linear damage falloff
+15% Critical Damage");
    }
    const int maxUseTime = 72;
    const int minUseTime = 14;
    const int maxMultishot = 5;
    public override void SetDefaults()
    {
        Item.damage = 21;
        Item.crit = 15;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 47;
        Item.height = 16;
        Item.useTime = maxUseTime;
        Item.useAnimation = maxUseTime;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1.5f;
        Item.value = Item.buyPrice(gold: 11);
        Item.rare = 9;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = WeaponCommon.ModifySoundStyle(new SoundStyle("WarframeMod/Sounds/KuvaKohmSound"), pitchVariance: 0.15f);
    }
    int lastShotTime = 0;
    int timeSinceLastShot = 60;
    int multishot = 1;
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentVortex, 11);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
    public override bool CanUseItem(Player player)
    {
        timeSinceLastShot = (int)(Main.time - lastShotTime);

        if (timeSinceLastShot < 24)
            multishot = maxMultishot > multishot ? multishot + 1 : maxMultishot;
        else multishot = 1;

        if (Item.useTime > minUseTime)
        {
            Item.useTime = Item.useTime * 2 / 3;
            Item.useAnimation = Item.useTime;

            if (Item.useTime < minUseTime)
            {
                Item.useTime = minUseTime;
                Item.useAnimation = minUseTime;
            }
        }
        else if (timeSinceLastShot > 16)
        {
            Item.useTime += timeSinceLastShot / 3;
            Item.useAnimation += timeSinceLastShot / 3;
            if (Item.useTime > maxUseTime)
            {
                Item.useTime = maxUseTime;
                Item.useAnimation = maxUseTime;
            }
        }
        lastShotTime = (int)Main.time;

        return base.CanUseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width + 5);
        for (int i = 0; i < multishot; i++)
        {
            float spread = (timeSinceLastShot > 46 ? 0.015f : 0.1f);
            var proj = Projectile.NewProjectileDirect(source, position, velocity.RotatedByRandom(spread), type, damage, knockback, player.whoAmI);
            {
                var globalProj = proj.GetGlobalProjectile<CustomProjectileDamageModifier>();
                proj.timeLeft = 120;
                int defaultTimeLeft = proj.timeLeft;
                globalProj.modifyDamage = (Projectile projectile, int oldDamage, Entity target) =>
                    oldDamage * projectile.timeLeft / defaultTimeLeft;
            }
            {
                var buffProj = proj.GetGlobalProjectile<BuffGlobalProjectile>();
                buffProj.bleedingChance = 0.35f;
            }
        }
        return false;
    }
}