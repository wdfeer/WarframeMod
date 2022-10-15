using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class Burston : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots a 3-round burst\n-20% Critical damage");
    }
    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.crit = 2;
        Item.knockBack = 1.5f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 40;
        Item.height = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 4;
        Item.useAnimation = 23;
        Item.useLimitPerAnimation = 3;
        Item.rare = 2;
        Item.value = Item.buyPrice(gold: 35);
        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 15f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(0, -0.5f);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item11, position);

        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Projectile projectile = Main.projectile[projectileID];
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = -1;
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 0.8f;
        return false;
    }
}