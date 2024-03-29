using Terraria.DataStructures;
using Terraria.Audio;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class RedeemerPrime : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 42;
        Item.crit = 20;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.width = 48;
        Item.height = 24;
        Item.scale = 1f;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 7;
        Item.value = 15000;
        Item.rare = 5;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.GoldenBullet;
        Item.shootSpeed = 16f;
        Item.UseSound = new SoundStyle("WarframeMod/Content/Sounds/RedeemerPrimeSound").ModifySoundStyle(pitchVariance: 0.1f);
    }
    public override float UseSpeedMultiplier(Player player)
    {
        return player.GetAttackSpeed(Item.DamageType);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Redeemer>());
        recipe.AddIngredient(ItemID.HallowedBar, 8);
        recipe.AddIngredient(ItemID.SoulofFright, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int i = 0; i < 6; i++)
        {
            var projectile = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.13f, Item.width);
            projectile.DamageType = DamageClass.Melee;
            projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.1f;
            projectile.GetGlobalProjectile<FalloffGlobalProjectile>().SetFalloff(projectile.position, 20 * 16, 40 * 16, 0.6f);
        }
        return false;
    }
}