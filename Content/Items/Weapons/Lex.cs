using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;
public class Lex : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Double stack to increase fire rate at the cost of accuracy");
    }
    public override void SetDefaults()
    {
        Item.damage = 72;
        Item.crit = 16;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 20;
        Item.useTime = 55;
        Item.useAnimation = 55;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 4;
        Item.value = Item.buyPrice(silver: 80);
        Item.rare = 3;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = false;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
        Item.maxStack = 2;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Handgun);
        recipe.AddIngredient(ItemID.TissueSample, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Handgun);
        recipe.AddIngredient(ItemID.ShadowScale, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public bool DoubleStack => Item.stack == 2;
    public override float UseSpeedMultiplier(Player player)
    {
        return DoubleStack ? 1.5f : 1f;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, DoubleStack ? 0.075f : 0.01f, Item.width);
        return false;
    }
}
