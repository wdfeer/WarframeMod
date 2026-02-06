using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Hind : ModItem
{
    public const int AMMO_SAVE_CHANCE = 80;
    public const int CRIT_DAMAGE_REDUCTION = 25;
    public const int DEFENSE_PENETRATION = 20;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(AMMO_SAVE_CHANCE, $"-{CRIT_DAMAGE_REDUCTION}%", DEFENSE_PENETRATION);

    public override void SetDefaults()
    {
        Item.damage = 22;
        Item.DamageType = DamageClass.Ranged;
        Item.crit = 3;
        Item.width = 32;
        Item.height = 13;
        Item.useTime = 2;
        Item.useLimitPerAnimation = 5;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0.2f;
        Item.value = Item.sellPrice(gold: 1, silver: 60);
        Item.rare = ItemRarityID.Lime;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1.5f, 0f);
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Main.rand.Next(0, 100) < AMMO_SAVE_CHANCE) return false;
        return base.CanConsumeAmmo(ammo, player);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        var sound = SoundID.Item41.WithVolumeScale(0.7f);
        SoundEngine.PlaySound(sound, position);
        Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, spread: 0.02f,
            spawnOffset: Item.width);
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = 1;
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier -= CRIT_DAMAGE_REDUCTION / 100f;
        proj.ArmorPenetration += DEFENSE_PENETRATION;
        return false;
    }
}