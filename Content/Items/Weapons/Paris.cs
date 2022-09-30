using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

internal class Paris : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 27;
        Item.crit = 26;
        Item.knockBack = 3.2f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 14;
        Item.height = 52;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 39;
        Item.useAnimation = 39;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 40);
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Arrow;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(4, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        
        return false;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<MK1Paris>());
        recipe.AddIngredient(ItemID.DemoniteBar, 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<MK1Paris>());
        recipe.AddIngredient(ItemID.CrimtaneBar, 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}