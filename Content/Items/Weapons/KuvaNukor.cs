using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class KuvaNukor : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.crit = 3;
        Item.DamageType = DamageClass.Magic;
        Item.channel = true;
        Item.width = 32;
        Item.height = 24;
        Item.useTime = 6;
        Item.useAnimation = 6;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = Item.buyPrice(gold: 10);
        Item.rare = 5;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<KuvaNukorProjectile>();
        Item.shootSpeed = 12f;
        Item.mana = 6;
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/KuvaNukorStartSound");
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 2.5f;
        return false;
    }
}