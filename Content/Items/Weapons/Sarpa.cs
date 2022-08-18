using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Sarpa : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Fires 5 rounds in a burst\n28% chance to proc Slash\nDamage Falloff starts at 25 tiles, stops after 50");
    }
    public override void SetDefaults()
    {
        Item.damage = 21;
        Item.crit = 10;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.noMelee = true;
        Item.width = 48;
        Item.height = 24;
        Item.scale = 1f;
        Item.useTime = 3;
        Item.useAnimation = 60;
        Item.useLimitPerAnimation = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 2;
        Item.value = Item.buyPrice(gold: 1);
        Item.rare = 3;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 16f;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4, 2);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Handgun, 1);
        recipe.AddIngredient(ItemID.MeteoriteBar, 8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item11, position);
        Projectile projectile = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.02f, Item.width);
        projectile.DamageType = DamageClass.Melee;
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = 2;
        projectile.GetGlobalProjectile<BuffGlobalProjectile>().bleedingChance = 0.28f;

        return false;
    }
}