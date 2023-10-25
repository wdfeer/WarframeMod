using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;

internal class KuvaAyanga : ModItem
{
    public const int AMMO_DAMAGE_DECREASE_PERCENT = 90;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"-{AMMO_DAMAGE_DECREASE_PERCENT}%");
    public override void SetDefaults()
    {
        Item.damage = 50;
        Item.crit = 31;
        Item.knockBack = 8;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 58;
        Item.height = 17;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item61.WithVolumeScale(0.8f);
        Item.useTime = 14;
        Item.useAnimation = 14;
        Item.autoReuse = true;
        Item.rare = 8;
        Item.value = Item.sellPrice(gold: 3);
        Item.shoot = ModContent.ProjectileType<TonkorProjectile>();
        Item.shootSpeed = 19f;
        Item.useAmmo = ItemID.Grenade;
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GrenadeLauncher);
        recipe.AddIngredient<Kuva>(3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyAmmoDamage(this, player, ref damage, 1 - (AMMO_DAMAGE_DECREASE_PERCENT / 100f));
        var proj = WeaponCommon.ShootWith(this, player, source, position, velocity, Item.shoot, damage, knockback, spread: 0.03f, spawnOffset: Item.width + 3);
        (proj.ModProjectile as TonkorProjectile).gravity += 0.2f;
        return false;
    }
}