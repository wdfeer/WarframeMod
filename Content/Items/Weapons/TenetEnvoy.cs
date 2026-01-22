using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Projectiles;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;

public class TenetEnvoy : ModItem
{
    public const int COLD_CHANCE = 20;
    public const int CRIT_DAMAGE_BOOST_PERCENT = 30;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(COLD_CHANCE, $"+{CRIT_DAMAGE_BOOST_PERCENT}%");
    public override void SetDefaults()
    {
        Item.damage = 733;
        Item.crit = 18;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 48;
        Item.height = 16;
        Item.useTime = 64;
        Item.useAnimation = 64;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 7;
        Item.value = Item.sellPrice(gold: 12);
        Item.rare = 10;
        Item.autoReuse = false;
        Item.shoot = 1;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Rocket;
        Item.UseSound = SoundID.Item61;
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentVortex, 12);
        recipe.AddIngredient<Fieldron>();
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var projectile = WeaponCommon.ShootWith(this, player, source, position, velocity, ModContent.ProjectileType<TenetEnvoyProjectile>(), damage, knockback, spawnOffset: Item.width);
        projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBuff(new Common.BuffChance(ModContent.BuffType<ColdDebuff>(), ColdDebuff.DEFAULT_DURATION, 20));
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier += CRIT_DAMAGE_BOOST_PERCENT / 100f;
        float rotation = Convert.ToSingle(-Math.Atan2(velocity.X, velocity.Y));
        projectile.rotation = rotation;

        return false;
    }
}