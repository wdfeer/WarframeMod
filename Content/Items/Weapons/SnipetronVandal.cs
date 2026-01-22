using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class SnipetronVandal : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 180;
        Item.crit = 24;
        Item.mana = 24;
        Item.DamageType = DamageClass.Magic;
        Item.width = 64;
        Item.height = 12;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6;
        Item.value = Item.buyPrice(gold: 33);
        Item.rare = 5;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 16f;
        Item.shoot = ProjectileID.LaserMachinegunLaser;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6, 0);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width * 0.75f);
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback);
        proj.penetrate += 3;
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = -1;
        return false;
    }
}