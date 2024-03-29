using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Baza : ModItem
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AMMO_SAVE_CHANCE);
    public const int AMMO_SAVE_CHANCE = 75;
    public const float AMMO_DAMAGE_MULT = 0.25f;
    public override void SetDefaults()
    {
        Item.damage = 6;
        Item.crit = 22;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 45;
        Item.height = 18;
        Item.useTime = 4;
        Item.useAnimation = 4;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = Item.sellPrice(gold: 2);
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
        if (Main.rand.Next(0, 100) <= AMMO_SAVE_CHANCE) return false;
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