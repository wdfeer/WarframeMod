using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class RaktaBallistica : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 9;
        Item.crit = 16;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 30;
        Item.height = 32;
        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1;
        Item.value = Item.sellPrice(gold: 1);
        Item.rare = 3;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = false;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16;
        Item.useAmmo = AmmoID.Arrow;
    }
    double lastShotTime = 0;
    double timeSinceLastShot = 60;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, 0.2f);
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        timeSinceLastShot = Main.time - lastShotTime;
        lastShotTime = Main.time;
        float chargeMult = (float)Math.Clamp(timeSinceLastShot / Item.useTime, 1, 2);
        for (int i = 0; i < 4; i++)
        {
            int projectileID = Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(7) / chargeMult), type, (int)(damage * chargeMult), knockback, player.whoAmI);
            Projectile projectile = Main.projectile[projectileID];
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        return false;
    }
}