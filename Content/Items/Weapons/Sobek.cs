using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class Sobek : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.crit = 7;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 42;
        Item.height = 14;
        Item.scale = 1.2f;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 3;
        Item.value = Item.sellPrice(gold: 2);
        Item.rare = 3;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item36.ModifySoundStyle(0.6f, 0.05f);
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo Item that this weapon uses. Note that this is not an Item Id, but just a magic value.
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DemoniteBar, 14);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrimtaneBar, 14);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(6, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, 0.5f);
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width - 6);
        for (int i = 0; i < 4; i++)
        {
            Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(8 * i)), type, damage, knockback, player.whoAmI);
        }
        return false;
    }
}