using Terraria.DataStructures;
using WarframeMod.Global;

namespace WarframeMod.Items.Weapons;
public class GorgonWraith : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots rapidly but inaccurately\n-5% Critical Damage\n60% Chance not to consume ammo");
    }
    public override void SetDefaults()
    {
        Item.damage = 8;
        Item.crit = 11;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 17;
        Item.height = 49;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 6);
        Item.rare = 3;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Main.rand.Next(0, 100) <= 60) return false;
        return true;
    }
    float lastShotTime = 0;
    float timeSinceLastShot = 60;
    public override bool CanUseItem(Player player)
    {
        timeSinceLastShot = (float)Main.time - lastShotTime;
        if (Item.useTime > 5)
        {
            Item.useTime -= 3;
            Item.useAnimation -= 3;
            if (Item.useTime < 5)
            {
                Item.useTime = 5;
                Item.useAnimation = 5;
            }
        }
        else if (timeSinceLastShot > 17)
        {
            Item.useTime += (int)(timeSinceLastShot / 3);
            Item.useAnimation += (int)(timeSinceLastShot / 3);
            if (Item.useTime > 19)
            {
                Item.useTime = 19;
                Item.useAnimation = 19;
            }
        }
        lastShotTime = (float)Main.time;

        return base.CanUseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        velocity = velocity.RotatedByRandom(0.05f);
        Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        var critProj = proj.GetGlobalProjectile<CritGlobalProjectile>();
        critProj.CritMultiplier = 0.95f;
        return false;
    }
}
