using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Snipetron : ModItem
{
    public const int CRIT_DAMAGE_DECREASE_PERCENT = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CRIT_DAMAGE_DECREASE_PERCENT);

    public override void SetDefaults()
    {
        Item.damage = 60;
        Item.crit = 26;
        Item.DamageType = DamageClass.Magic;
        Item.width = 60;
        Item.height = 12;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6;
        Item.value = Item.buyPrice(gold: 8);
        Item.rare = 3;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 16f;
        Item.shoot = ProjectileID.LaserMachinegunLaser;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6, 0);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width * 0.75f);
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier -= CRIT_DAMAGE_DECREASE_PERCENT / 100f;
        proj.penetrate += 2;
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = -1;
        return false;
    }
}