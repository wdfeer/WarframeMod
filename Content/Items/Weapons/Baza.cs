using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Baza : ModItem
{
    public const float AMMO_DAMAGE_MULT = 0.25f;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"75% Chance not to consume ammo\n+50% Critical Damage\n-{(int)((1 - AMMO_DAMAGE_MULT) * 100f)}% ammo damage");
    }
    public override void SetDefaults()
    {
        Item.damage = 5;
        Item.crit = 22;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 45;
        Item.height = 18;
        Item.useTime = 4;
        Item.useAnimation = 4;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = 15000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item11.ModifySoundStyle(0.1f);
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo Item that this weapon uses. Note that this is not an Item Id, but just a magic value.
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4, 0);
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Main.rand.Next(0, 100) <= 75) return false;
        return base.CanConsumeAmmo(ammo, player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, AMMO_DAMAGE_MULT);
        Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        var critProj = projectile.GetGlobalProjectile<CritGlobalProjectile>();
        critProj.CritMultiplier = 1.5f;
        return false;
    }
}