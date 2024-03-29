using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class Nataruk : ModItem
{
    public const int BASE_CRIT_UNCHARGED = 20;
    public const int BASE_CRIT_CHARGED = 50;
    public const int BASE_CRIT_PERFECT = 60;
    public const float CRIT_MULT_UNCHARGED = 0.9f;
    public const float CRIT_MULT_CHARGED = 1.1f;
    public const float CRIT_MULT_PERFECT = 1.2f;
    public override void SetDefaults()
    {
        Item.damage = 200;
        Item.crit = BASE_CRIT_UNCHARGED - 4;
        Item.knockBack = 7f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 29;
        Item.height = 64;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.rare = 10;
        Item.value = Item.sellPrice(gold: 3);
        Item.shoot = ModContent.ProjectileType<NatarukProjectile>();
        Item.shootSpeed = 16f;
        Item.channel = true;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-3, 0);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DD2BetsyBow);
        recipe.AddIngredient(ItemID.Tsunami);
        recipe.AddIngredient(ItemID.FairyQueenRangedItem);
        recipe.AddIngredient(ItemID.Phantasm);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}