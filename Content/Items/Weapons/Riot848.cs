using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Riot848 : ModItem
{
    public const int WEAK_CHANCE = 8;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(WEAK_CHANCE);

    public override void SetDefaults()
    {
        Item.damage = 51;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 17;
        Item.useTime = 8;
        Item.useAnimation = 8;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1f;
        Item.value = Item.sellPrice(gold: 18);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item11.WithVolumeScale(0.7f);
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Uzi);
        recipe.AddIngredient(ItemID.LunarBar, 10);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        this.ShootWith(player, source, position, velocity, ModContent.ProjectileType<Riot848Projectile>(), damage, knockback, 0.02f, Item.width / 2f);
        return false;
    }
}