using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WarframeMod.Global;

namespace WarframeMod.Items.Weapons;

public class Velocitus : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+80% Critical Damage");
    }
    public override void SetDefaults()
    {
        Item.damage = 197;
        Item.crit = 56;
        Item.DamageType = DamageClass.Ranged;
        Item.channel = true;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 16;
        Item.value = Item.buyPrice(gold: 9);
        Item.rare = ItemRarityID.Cyan;
        Item.autoReuse = false;
        Item.useAmmo = AmmoID.Bullet;
        Item.shoot = ModContent.ProjectileType<VelocitusProjectile>();
        Item.shootSpeed = 12f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentVortex, 16);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, 80);
        Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<VelocitusProjectile>(), damage, knockback, player.whoAmI);
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.8f;
        return false;
    }
}