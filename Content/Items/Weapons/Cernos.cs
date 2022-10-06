using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

internal class Cernos : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots high velocity arrows with extra penetration");
    }
    public override void SetDefaults()
    {
        Item.damage = 31;
        Item.crit = 32;
        Item.knockBack = 7;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 36;
        Item.height = 54;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 1);
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Arrow;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4, 0);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Paris>());
        recipe.AddIngredient(ItemID.HellstoneBar, 7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Projectile projectile = Main.projectile[projectileID];
        if (projectile.penetrate != -1) projectile.penetrate++;
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = -1;
        (Mod as WarframeMod).SetProjectileExtraUpdatesNetSafe(projectileID, projectile.extraUpdates + 1);
        return false;
    }
}