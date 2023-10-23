using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class BazaPrime : ModItem
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AMMO_SAVE_CHANCE);
    public const int AMMO_SAVE_CHANCE = 80;
    public override void SetDefaults()
    {
        Item.damage = 23;
        Item.crit = 24;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 45;
        Item.height = 18;
        Item.useTime = 4;
        Item.useAnimation = 4;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = 1500;
        Item.rare = 7;
        Item.UseSound = SoundID.Item11.WithVolumeScale(0.2f);
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 17f;
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
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
        this.ModifyAmmoDamage(player, ref damage, 0.5f);
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.001f, Item.width);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.5f;
        return false;
    }
}