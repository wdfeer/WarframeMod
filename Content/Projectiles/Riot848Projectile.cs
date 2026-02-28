using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class Riot848Projectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MoonlordBullet;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.MoonlordBullet);
        AIType = ProjectileID.MoonlordBullet;
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddBuff(StackableBuff.Weak, Riot848.WEAK_CHANCE);
        Projectile.timeLeft = 20 * 60;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = Vector2.Zero;
        return false;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        Projectile.velocity = Vector2.Zero;
    }
}