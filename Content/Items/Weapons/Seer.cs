using Terraria.DataStructures;


namespace WarframeMod.Content.Items.Weapons;
public class Seer : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 24;
        Item.crit = 1;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 20;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 4;
        Item.value = Item.sellPrice(silver: 50);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = false;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-0.5f, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.01f, Item.width);
        proj.extraUpdates += 1;
        return false;
    }
}
