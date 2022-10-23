using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class ScourgePrime : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault(@"Fires a projectile that splits into multiple projectiles on impact
20% chance to inflict ichor and cursed flames");
    }
    public override void SetDefaults()
    {
        Item.damage = 47;
        Item.crit = 6;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 95;
        Item.height = 16;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 4);
        Item.rare = 5;
        Item.UseSound = SoundID.Item43;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.ScourgePrimeProjectile>();
        Item.shootSpeed = 16f;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-20, 0);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Scourge>());
        recipe.AddIngredient(ItemID.SoulofFright, 5);
        recipe.AddIngredient(ItemID.SoulofNight, 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width * 0.6f);
        var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        var buffer = projectile.GetGlobalProjectile<BuffGlobalProjectile>();
        buffer.AddBuff(new Common.BuffChance(BuffID.Ichor, 460, 0.25f));
        buffer.AddBuff(new Common.BuffChance(BuffID.CursedInferno, 460, 0.25f));
        return false;
    }
}