using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class Scourge : ModItem
{
    public const int DEFENSE_PENETRATION = 10;
    public override void SetDefaults()
    {
        Item.damage = 15;
        Item.crit = 0;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 8;
        Item.width = 95;
        Item.height = 15;
        Item.scale = 1f;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 1);
        Item.rare = 3;
        Item.UseSound = SoundID.Item43;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.ScourgeProjectile>();
        Item.shootSpeed = 11f;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-20, -1);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TungstenBar, 9);
        recipe.AddIngredient(ItemID.Emerald, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SilverBar, 9);
        recipe.AddIngredient(ItemID.Emerald, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width * 0.75f);
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        return false;
    }
}