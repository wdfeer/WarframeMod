using WarframeMod.Content.Projectiles;
using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;
internal class Lenz : ModItem
{
    public const int COLD_DURATION = 144;
    public override void SetDefaults()
    {
        Item.damage = 586;
        Item.crit = 46;
        Item.knockBack = 3;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 30;
        Item.height = 48;
        Item.scale = 1.2f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 66;
        Item.useAnimation = 66;
        Item.rare = ItemRarityID.Cyan;
        Item.value = 120000;
        Item.shoot = ModContent.ProjectileType<LenzProjArrow>();
        Item.shootSpeed = 24f;
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Tsunami, 1);
        recipe.AddIngredient(ItemID.GrenadeLauncher, 1);
        recipe.AddIngredient(ItemID.IceRod);
        recipe.AddIngredient(ModContent.ItemType<Fieldron>());
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = WeaponCommon.ShootWith(this, player, source, position, velocity, type, damage, knockback, spawnOffset: 23);
        return false;
    }
}