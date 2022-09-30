using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

internal class MK1Paris : ModItem
{
    public override string Texture => "WarframeMod/Content/Items/Weapons/Paris";
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("MK1-Paris");
    }
    public override void SetDefaults()
    {
        Item.damage = 5;
        Item.crit = 26;
        Item.knockBack = 2;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 14;
        Item.height = 52;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 33;
        Item.useAnimation = 33;
        Item.rare = 1;
        Item.value = Item.buyPrice(silver: 15);
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Arrow;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(4, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        return false;
    }
}