using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Global;
using WarframeMod.Projectiles;

namespace WarframeMod.Items.Weapons;

public class Kohm : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Takes a while to spool up while increasing Multishot up to 5 pellets\n33% chance to apply bleeding on hit\nLinear damage falloff\n+15% Critical Damage");
    }
    const int maxUseTime = 82;
    const int minUseTime = 17;
    const int maxMultishot = 5;
    public override void SetDefaults()
    {
        Item.damage = 5;
        Item.crit = 7;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 47;
        Item.height = 16;
        Item.useTime = maxUseTime;
        Item.useAnimation = maxUseTime;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1;
        Item.value = Item.buyPrice(gold: 6);
        Item.rare = 3;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = WeaponCommon.ModifySoundStyle(new SoundStyle("WarframeMod/Sounds/KuvaKohmSound"), pitchVariance: 0.15f);
    }
    int lastShotTime = 0;
    int timeSinceLastShot = 60;
    int multishot = 1;
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
                var globalProj = proj.GetGlobalProjectile<WarframeGlobalProjectile>();
                proj.timeLeft = 120;
                int defaultTimeLeft = proj.timeLeft;
                globalProj.modifyDamage = (Projectile projectile, int oldDamage, Entity target) =>
                    oldDamage * projectile.timeLeft / defaultTimeLeft;
            }
            {
                var buffProj = proj.GetGlobalProjectile<BuffGlobalProjectile>();
                buffProj.bleedingChance = 0.33f;
            }
        }
        return false;
    }
}