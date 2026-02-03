using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Arcanes;

public class LongbowSharpshot : Arcane
{
    public const int MIN_DISTANCE = 50 * 16;
    public const int DAMAGE_INCREASE = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MIN_DISTANCE / 16, DAMAGE_INCREASE);

    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<LongbowSharpshotPlayer>().enabled = true;
    }
}

class LongbowSharpshotPlayer : ModPlayer
{
    public bool enabled;

    public override void ResetEffects()
    {
        enabled = false;
    }

    private int[] moreArrowProjectiles =>
    [
        ModContent.ProjectileType<LenzProjArrow>(),
        ModContent.ProjectileType<KuvaBrammaProjectile>(),
    ];

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (enabled && (proj.arrow || moreArrowProjectiles.Contains(proj.type)) &&
            target.Distance(Player.position) > LongbowSharpshot.MIN_DISTANCE)
            Player.AddBuff(ModContent.BuffType<LongbowSharpshotBuff>(), 30 * 60);
    }

    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (Player.HasBuff<LongbowSharpshotBuff>() && IsBow(item))
        {
            damage += LongbowSharpshot.DAMAGE_INCREASE / 100f;
        }
    }

    public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage,
        float knockback)
    {
        if (Player.HasBuff<LongbowSharpshotBuff>() && IsBow(item))
        {
            Player.ClearBuff(ModContent.BuffType<LongbowSharpshotBuff>());
        }

        return base.Shoot(item, source, position, velocity, type, damage, knockback);
    }

    private int[] moreBows =>
    [
        ModContent.ItemType<Lenz>(), ModContent.ItemType<PrismaLenz>(), ModContent.ItemType<KuvaBramma>()
    ];

    private bool IsBow(Item item) => item.useAmmo == AmmoID.Arrow || moreBows.Contains(item.type);
}