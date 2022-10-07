using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Spectra : ModItem
{
    public const int DEFENSE_PENETRATION = 8;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"40% bleeding chance\n{DEFENSE_PENETRATION} defense penetration");
    }
    public override void SetDefaults()
    {
        Item.damage = 7;
        Item.crit = 10;
        Item.mana = 2;
        Item.DamageType = DamageClass.Magic;
        Item.channel = true;
        Item.width = 32;
        Item.height = 26;
        Item.useTime = 5;
        Item.useAnimation = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0.1f;
        Item.value = Item.sellPrice(silver: 50);
        Item.rare = 3;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<SpectraProjectile>();
        Item.shootSpeed = 16f;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2, 0);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wire, 40);
        recipe.AddIngredient(ItemID.MeteoriteBar, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width - 4);
        Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBleed(0.4f);
        return false;
    }
}