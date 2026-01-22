using WarframeMod.Content.Projectiles;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;
internal class PrismaLenz : ModItem
{
    public const int CRIT_DAMAGE_INCREASE_PERCENT = 40;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRIT_DAMAGE_INCREASE_PERCENT}%");
    public const int COLD_DURATION = 144;
    public override void SetDefaults()
    {
        Item.damage = 700;
        Item.crit = 46;
        Item.knockBack = 6;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 30;
        Item.height = 48;
        Item.scale = 1.2f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 48;
        Item.useAnimation = 48;
        Item.rare = ItemRarityID.Red;
        Item.value = Item.buyPrice(gold: 70);
        Item.shoot = ModContent.ProjectileType<LenzProjArrow>();
        Item.shootSpeed = 24f;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = WeaponCommon.ShootWith(this, player, source, position, velocity, type, damage, knockback, spawnOffset: 23);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1f + (CRIT_DAMAGE_INCREASE_PERCENT / 100f);
        return false;
    }
}