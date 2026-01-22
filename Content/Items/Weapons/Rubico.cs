using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Rubico : ModItem
{
    public const int CRIT_DAMAGE_INCREASE_PERCENT = 50;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CRIT_DAMAGE_INCREASE_PERCENT);

    public override void SetDefaults()
    {
        Item.damage = 80;
        Item.crit = 26;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 56;
        Item.height = 13;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6;
        Item.value = Item.sellPrice(gold: 2);
        Item.rare = 4;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 16f;
        Item.shoot = 10;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-3, 0);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width * 0.75f);
        var proj = this.ShootWith(player, source, position, velocity, ProjectileID.SniperBullet, damage, knockback, spawnOffset: 8);
        proj.friendly = true;
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier += CRIT_DAMAGE_INCREASE_PERCENT / 100f;
        return false;
    }
}