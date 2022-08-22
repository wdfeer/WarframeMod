using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Nukor : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+100% Critical Damage");
    }
    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.crit = -2;
        Item.DamageType = DamageClass.Magic;
        Item.channel = true;
        Item.width = 32;
        Item.height = 24;
        Item.useTime = 7;
        Item.useAnimation = 7;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = Item.buyPrice(silver: 45);
        Item.rare = 3;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<NukorProjectile>();
        Item.shootSpeed = 12f;
        Item.mana = 3;
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/KuvaNukorStartSound");
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DemoniteBar, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrimtaneBar, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        return false;
    }
}