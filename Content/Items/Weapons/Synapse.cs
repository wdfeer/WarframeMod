using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Synapse : ModItem
{
    private const int CRIT_DAMAGE_BOOST = 35;
    private const int ICHOR_CHANCE = 13;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRIT_DAMAGE_BOOST}%", ICHOR_CHANCE);

    public override void SetDefaults()
    {
        Item.damage = 21;
        Item.crit = 31;
        Item.mana = 5;
        Item.DamageType = DamageClass.Magic;
        Item.channel = true;
        Item.width = 48;
        Item.height = 11;
        Item.useTime = 5;
        Item.useAnimation = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = Item.buyPrice(gold: 1, silver: 20);
        Item.rare = 4;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<SynapseProjectile>();
        Item.shootSpeed = 16f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AdamantiteBar, 8);
        recipe.AddIngredient(ItemID.SoulofNight, 15);
        recipe.AddIngredient(ItemID.Ichor, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TitaniumBar, 12);
        recipe.AddIngredient(ItemID.SoulofNight, 20);
        recipe.AddIngredient(ItemID.Ichor, 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        Projectile projectile =
            Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier += CRIT_DAMAGE_BOOST / 100f;
        projectile.GetGlobalProjectile<BuffGlobalProjectile>().buffChances
            .Add(new BuffChance(BuffID.Ichor, 150, ICHOR_CHANCE));
        return false;
    }
}