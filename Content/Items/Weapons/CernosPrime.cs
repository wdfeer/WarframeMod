using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

internal class CernosPrime : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 250;
        Item.crit = 31;
        Item.knockBack = 9;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 42;
        Item.height = 64;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(gold: 22);
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4, 0);
    }

    public override float UseSpeedMultiplier(Player player) => player.GetAttackSpeed<RangedDamageClass>();

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback,
            spawnOffset: Item.width / 2f);
        (Mod as WarframeMod).SetProjectileExtraUpdatesNetSafe(proj, proj.extraUpdates + 1);
        return false;
    }
}