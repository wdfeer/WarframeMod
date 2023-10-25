using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class KuvaBramma : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 419;
        Item.crit = 31;
        Item.knockBack = 3;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 30;
        Item.height = 48;
        Item.scale = 1.2f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item38;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.rare = ItemRarityID.Red;
        Item.value = 120000;
        Item.shoot = ModContent.ProjectileType<KuvaBrammaProjectile>();
        Item.shootSpeed = 20f;
        Item.useAmmo = ItemID.Grenade;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6, 0);
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Phantasm, 1);
        recipe.AddIngredient(ItemID.GrenadeLauncher, 1);
        recipe.AddIngredient<Kuva>(5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = WeaponCommon.ShootWith(this, player, source, position, velocity, ModContent.ProjectileType<KuvaBrammaProjectile>(), damage, knockback, spawnOffset: Item.width - 6);

        return false;
    }
}