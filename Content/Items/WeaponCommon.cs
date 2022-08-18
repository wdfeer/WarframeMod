using Terraria.Audio;
using Terraria.DataStructures;

namespace WarframeMod.Content.Items;

public static class WeaponCommon
{
    public static SoundStyle ModifySoundStyle(this SoundStyle style, float volume = 1f, float pitchVariance = 0f)
    {
        style.Volume *= volume;
        style.PitchVariance = pitchVariance;
        return style;
    }
    public static void ModifyProjectileSpawnPosition(ref Vector2 position, Vector2 velocity, float offset)
    {
        Vector2 muzzleOffset = velocity.SafeNormalize(Vector2.Zero) * offset;
        if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            position += muzzleOffset;
    }
    public static Projectile ShootWith(this ModItem item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float spread = 0, float spawnOffset = 0)
    {
        ModifyProjectileSpawnPosition(ref position, velocity, spawnOffset);
        velocity = velocity.RotatedByRandom(spread);
        Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        return proj;
    }
}
