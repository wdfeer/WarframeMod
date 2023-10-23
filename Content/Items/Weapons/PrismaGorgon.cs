using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class PrismaGorgon : ModItem
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AMMO_SAVE_CHANCE);
    public const int AMMO_SAVE_CHANCE = 75;
    public override void SetDefaults()
    {
        Item.damage = 61;
        Item.crit = 26;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 17;
        Item.height = 51;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2;
        Item.value = Item.buyPrice(gold: 70);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Main.rand.Next(0, 100) <= AMMO_SAVE_CHANCE) return false;
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
        else if (timeSinceLastShot > 21)
        {
            Item.useTime += (int)(timeSinceLastShot / 3);
            Item.useAnimation += (int)(timeSinceLastShot / 3);
            if (Item.useTime > 16)
            {
                Item.useTime = 16;
                Item.useAnimation = 16;
            }
        }
        lastShotTime = (float)Main.time;

        return base.CanUseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        velocity = velocity.RotatedByRandom(timeSinceLastShot > 21 ? 0 : 0.06f);
        Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.15f;
        return false;
    }
}
