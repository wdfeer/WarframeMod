using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

internal class RaktaCernos : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots high velocity arrows");
    }
    public override void SetDefaults()
    {
        Item.damage = 48;
        Item.crit = 31;
        Item.knockBack = 4;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 41;
        Item.height = 64;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 2);
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
        recipe.AddIngredient(ItemID.SoulofNight, 17);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        int projectileID = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Projectile projectile = Main.projectile[projectileID];
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = -1;
        (Mod as WarframeMod).SetProjectileExtraUpdatesNetSafe(projectileID, projectile.extraUpdates + 1);
        return false;
    }
}