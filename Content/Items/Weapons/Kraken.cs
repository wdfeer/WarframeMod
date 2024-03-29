using Terraria.Audio;
using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

internal class Kraken : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.crit = 1;
        Item.knockBack = 1.25f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 32;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 3;
        Item.useAnimation = 27;
        Item.useLimitPerAnimation = 2;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 90);
        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 12f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(4f, -1f);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteoriteBar, 2);
        recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
        recipe.AddIngredient(ItemID.JungleSpores, 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item11, position);
        this.ShootWith(player, source, position, velocity * 0.9f, type, damage, knockback, 0.01f, Item.width);
        return false;
    }
}