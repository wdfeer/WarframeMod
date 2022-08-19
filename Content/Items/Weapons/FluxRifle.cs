using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class FluxRifle : ModItem
{
    public const int DEFENSE_PENETRATION = 9;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"Doesn't consume ammo\n{DEFENSE_PENETRATION} Defense penetration");
    }
    public override void SetDefaults()
    {
        Item.damage = 9;
        Item.crit = 6;
        Item.channel = true;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 45;
        Item.height = 16;
        Item.useTime = 5;
        Item.useAnimation = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = Item.sellPrice(gold: 2);
        Item.rare = 3;
        Item.autoReuse = true;
        Item.shootSpeed = 16f;
        Item.shoot = ModContent.ProjectileType<Projectiles.FluxRifleProjectile>();
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4, 0);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PlatinumBar, 9);
        recipe.AddIngredient(ItemID.TissueSample, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GoldBar, 9);
        recipe.AddIngredient(ItemID.TissueSample, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PlatinumBar, 9);
        recipe.AddIngredient(ItemID.ShadowScale, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.GoldBar, 9);
        recipe.AddIngredient(ItemID.ShadowScale, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        return false;
    }
}