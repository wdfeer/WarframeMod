using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
internal class Tonkor : ModItem
{
    public const int CRIT_DAMAGE_INCREASE_PERCENT = 25;
    public const int AMMO_DAMAGE_DECREASE_PERCENT = 90;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRIT_DAMAGE_INCREASE_PERCENT}%", $"-{AMMO_DAMAGE_DECREASE_PERCENT}%");
    public override void SetDefaults()
    {
        Item.damage = 23;
        Item.crit = 21;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 37;
        Item.height = 14;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item61;
        Item.useTime = 66;
        Item.useAnimation = 66;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 50);
        Item.shoot = ModContent.ProjectileType<TonkorProjectile>();
        Item.shootSpeed = 16f;
        Item.useAmmo = ItemID.Grenade;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyAmmoDamage(this, player, ref damage, 1 - (AMMO_DAMAGE_DECREASE_PERCENT / 100f));
        var proj = WeaponCommon.ShootWith(this, player, source, position, velocity, Item.shoot, damage, knockback, spawnOffset: Item.width + 2);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1f + CRIT_DAMAGE_INCREASE_PERCENT / 100f;
        (proj.ModProjectile as TonkorProjectile).gravity += 0.2f;
        return false;
    }
}