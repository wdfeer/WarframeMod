using Terraria.Audio;
using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class Boar : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("An extremely inaccurate automatic shotgun, shoots 4 pellets at once\n-75% ammo damage");
    }

    public override void SetDefaults()
    {
        Item.damage = 8;
        Item.crit = 6;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 40;
        Item.height = 17;
        Item.scale = 1.2f;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 1;
        Item.value = Item.buyPrice(silver: 60);
        Item.rare = 2;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = new SoundStyle("WarframeMod/Content/Sounds/BoarPrimeSound").ModifySoundStyle(0.6f);
        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 12;
        Item.autoReuse = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LeadBar, 17);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.IronBar, 17);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, 0.25f);
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        for (int i = 0; i < 4; i++)
        {
            int projectileID = Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(15 * (i + 1))), type, damage, knockback, player.whoAmI);
            Projectile proj = Main.projectile[projectileID];
        }

        return false;
    }
}