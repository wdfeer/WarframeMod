using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Redeemer : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 13;
        Item.crit = 10;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.width = 48;
        Item.height = 24;
        Item.scale = 1f;
        Item.useTime = 72;
        Item.useAnimation = 72;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 3;
        Item.value = 15000;
        Item.rare = ItemRarityID.Green;
        Item.autoReuse = false;
        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 12f;
        Item.UseSound = new SoundStyle("WarframeMod/Content/Sounds/RedeemerPrimeSound").ModifySoundStyle(pitchVariance: 0.06f);
    }
    public override float UseSpeedMultiplier(Player player)
    {
        return MathF.Pow(player.GetAttackSpeed(Item.DamageType), 2);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        for (int i = 0; i < 6; i++)
        {
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(15)), type, damage, knockback, player.whoAmI);
            projectile.DamageType = DamageClass.Melee;
            projectile.GetGlobalProjectile<FalloffGlobalProjectile>().SetFalloff(projectile.position, 30 * 16, 50 * 16, 0.6f);
        }

        return false;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup(RecipeGroupID.IronBar, 9);
        recipe.AddIngredient(ItemID.IllegalGunParts, 1);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}