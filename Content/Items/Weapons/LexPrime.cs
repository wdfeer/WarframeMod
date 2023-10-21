using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;
public class LexPrime : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 130;
        Item.crit = 21;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 34;
        Item.height = 24;
        Item.useTime = 29;
        Item.useAnimation = 29;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 5;
        Item.value = Item.buyPrice(gold: 2);
        Item.rare = 5;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = false;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
        Item.maxStack = 2;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1.25f, 0);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Lex>());
        recipe.AddIngredient(ItemID.SoulofMight, 2);
        recipe.AddIngredient(ItemID.SoulofSight, 2);
        recipe.AddIngredient(ItemID.SoulofFright, 2);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public bool DoubleStack => Item.stack == 2;
    public override float UseSpeedMultiplier(Player player)
    {
        return DoubleStack ? 1.3f : 1f;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, DoubleStack ? 0.06f : 0.01f, Item.width);
        return false;
    }
}
