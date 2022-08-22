using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items;

namespace WarframeMod.Content.Items.Weapons;
public class Supra : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Rapidly shoots nano bullets\n-10% Critical Damage\n69% Chance not to consume ammo");
    }
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/SupraVandalSound").ModifySoundStyle(pitchVariance: 0.1f);
        Item.damage = 47;
        Item.crit = 8;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 17;
        Item.height = 48;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2;
        Item.value = Item.buyPrice(gold: 5);
        Item.rare = 9;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.MartianWalkerLaser;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Main.rand.Next(0, 100) <= 69) return false;
        return base.CanConsumeAmmo(ammo, player);
    }
    int lastShotTime = 0;
    int timeSinceLastShot = 60;
    public override bool CanUseItem(Player player)
    {
        timeSinceLastShot = (int)Main.time - lastShotTime;
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
        else if (timeSinceLastShot > 15)
        {
            Item.useTime += timeSinceLastShot / 3;
            Item.useAnimation += timeSinceLastShot / 3;
            if (Item.useTime > 16)
            {
                Item.useTime = 16;
                Item.useAnimation = 16;
            }
        }
        lastShotTime = (int)Main.time;

        return base.CanUseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, ProjectileID.NanoBullet, damage, knockback, timeSinceLastShot > 30 ? 0 : 0.065f, 50);
        proj.extraUpdates = 2;
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 0.9f;
        return false;
    }
}