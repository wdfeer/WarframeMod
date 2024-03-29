using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
internal class Acceltra : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 17;
        Item.crit = 28;
        Item.knockBack = 3.6f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 58;
        Item.height = 17;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 5;
        Item.useAnimation = 5;
        Item.autoReuse = true;
        Item.rare = 5;
        Item.value = Item.buyPrice(gold: 6);
        Item.shoot = ModContent.ProjectileType<AcceltraProjectile>();
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/AcceltraSound").ModifySoundStyle(pitchVariance: 0.15f);
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(2, 4);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, 0.5f);
        var proj = this.ShootWith(player, source, position, velocity, ModContent.ProjectileType<AcceltraProjectile>(), damage, knockback, spread: 0.02f, spawnOffset: Item.width);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.4f;
        AcceltraProjectile modProj = proj.ModProjectile as AcceltraProjectile;
        modProj.initialPos = proj.position;
        return false;
    }
}