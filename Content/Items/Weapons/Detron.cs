using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Detron : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 11;
        Item.crit = 0;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 17;
        Item.width = 32;
        Item.height = 16;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 1;
        Item.value = Item.buyPrice(silver: 90);
        Item.rare = 3;
        Item.UseSound = SoundID.Item91.ModifySoundStyle(pitchVariance: 0.06f);
        Item.shoot = ProjectileID.LaserMachinegunLaser;
        Item.shootSpeed = 16;
        Item.autoReuse = true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        velocity *= 0.75f;
        for (int i = 0; i < 7; i++)
        {
            Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.14f, Item.width);
            proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 0.75f;
            proj.GetGlobalProjectile<BuffGlobalProjectile>().buffChances.Add(new Common.BuffChance(BuffID.Confused, 60, 0.12f));
        }

        return false;
    }
}