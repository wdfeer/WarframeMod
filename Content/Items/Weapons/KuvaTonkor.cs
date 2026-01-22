using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;

internal class KuvaTonkor : ModItem
{
    public const int CRIT_DAMAGE_INCREASE_PERCENT = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRIT_DAMAGE_INCREASE_PERCENT}%");
    public override void SetDefaults()
    {
        Item.damage = 63;
        Item.crit = 26;
        Item.knockBack = 9;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 37;
        Item.height = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item61;
        Item.useTime = 56;
        Item.useAnimation = 56;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.buyPrice(gold: 2, silver: 40);
        Item.shoot = ModContent.ProjectileType<TonkorProjectile>();
        Item.shootSpeed = 17f;
        Item.useAmmo = ItemID.Grenade;
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient<Tonkor>();
        recipe.AddIngredient<Kuva>(3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = WeaponCommon.ShootWith(this, player, source, position, velocity, ModContent.ProjectileType<TonkorProjectile>(), damage, knockback, spawnOffset: Item.width + 2);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1f + CRIT_DAMAGE_INCREASE_PERCENT / 100f;
        return false;
    }
}