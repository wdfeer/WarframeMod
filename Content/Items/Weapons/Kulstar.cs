using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class Kulstar : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 60;
        Item.crit = 13;
        Item.knockBack = 10f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 40;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 3);
        Item.shoot = ProjectileID.RocketI;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = SoundID.Item61;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(3.5f, -1f);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Kraken>());
        recipe.AddIngredient(ItemID.CobaltBar, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Kraken>());
        recipe.AddIngredient(ItemID.MythrilBar, 4);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ShootWith(player, source, position, velocity, ModContent.ProjectileType<KulstarProjectile>(), damage, knockback, 0.03f, Item.width);
        return false;
    }
}