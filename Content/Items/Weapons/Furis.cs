using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;
public class Furis : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 3;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 24;
        Item.height = 21;
        Item.useTime = 6;
        Item.useAnimation = 6;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = 1500;
        Item.rare = 2;
        Item.UseSound = SoundID.Item11.WithVolumeScale(0.8f);
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.005f, Item.width);
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = 3;
        return false;
    }
}