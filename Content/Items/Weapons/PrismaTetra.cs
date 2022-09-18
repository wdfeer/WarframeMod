using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;
public class PrismaTetra : ModItem
{
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/TenetTetraPrimarySound").ModifySoundStyle(pitchVariance: 0.12f);
        Item.damage = 55;
        Item.crit = 6;
        Item.mana = 9;
        Item.DamageType = DamageClass.Magic;
        Item.width = 40;
        Item.height = 14;
        Item.useTime = 8;
        Item.useAnimation = 8;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 4;
        Item.value = Item.buyPrice(gold: 7);
        Item.rare = 6;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.LaserMachinegunLaser;
        Item.shootSpeed = 16f;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(3, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.04f, Item.width);
        (Mod as WarframeMod).SetProjectileExtraUpdatesNetSafe(proj, 2);
        return false;
    }
}