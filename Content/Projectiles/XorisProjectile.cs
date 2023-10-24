using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

internal class XorisProjectile : GlaiveProjectile
{
    public override string Texture => "WarframeMod/Content/Items/Weapons/Xoris";
    public void SetBigBoom()
    {
        bigBoom = true;
        Projectile.damage *= Xoris.BIG_BOOM_DAMAGE_MULT;
    }
    bool bigBoom = false;
    public override int ExplosionWidth => bigBoom ? 480 : 320;
    public override float ExplosionSoundVolume => (bigBoom ? 1.4f : 1f) * base.ExplosionSoundVolume;
    public override void AI()
    {
        if (exploding)
            return;
        for (int num = 0; num < 2; num++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            dust.noLight = true;
            dust.scale = Main.rand.Next(80, 130) * 0.01f;
            dust.velocity *= 0.2f;
            dust.noGravity = true;
        }
    }
    public override void ExplosionDusts()
    {
        for (int i = 0; i < (bigBoom ? 120 : 30); i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            dust.scale = 1.5f;
            dust.noGravity = true;
            dust.velocity = Vector2.Normalize(dust.position - Projectile.Center) * 7;

        }
    }
}