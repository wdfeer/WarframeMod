using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Gammacor : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"20% chance to shortly confuse enemies");
    }
    public override void SetDefaults()
    {
        Item.damage = 33;
        Item.crit = 4;
        Item.mana = 4;
        Item.DamageType = DamageClass.Magic;
        Item.channel = true;
        Item.width = 32;
        Item.height = 20;
        Item.scale = 0.6f;
        Item.useTime = 5;
        Item.useAnimation = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0f;
        Item.value = Item.sellPrice(gold: 4);
        Item.rare = 4;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<GammacorProjectile>();
        Item.shootSpeed = 16f;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-5, 2);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width - 4);
        Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        projectile.GetGlobalProjectile<BuffGlobalProjectile>().Add(new Common.BuffChance(BuffID.Confused, 180, 0.2f));
        return false;
    }
}