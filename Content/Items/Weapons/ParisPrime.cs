using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

internal class ParisPrime : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots high velocity arrows");
    }
    public override void SetDefaults()
    {
        Item.damage = 90;
        Item.crit = 41;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 19;
        Item.height = 54;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 42;
        Item.useAnimation = 42;
        Item.autoReuse = true;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 3);
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Arrow;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(3, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        (Mod as WarframeMod).SetProjectileExtraUpdatesNetSafe(projectile, projectile.extraUpdates + 2);
        return false;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Paris>());
        recipe.AddIngredient(ItemID.HallowedBar, 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}