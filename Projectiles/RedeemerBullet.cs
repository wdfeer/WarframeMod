using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace WarframeMod.Projectiles
{
	/// <summary>
	/// This the class that clones the vanilla Meowmere projectile using CloneDefaults().
	/// Make sure to check out <see cref="ExampleCloneWeapon" />, which fires this projectile; it itself is a cloned version of the Meowmere.
	/// </summary>
	public class RedeemerBullet : ModProjectile
	{
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;
        public override void SetStaticDefaults() {
			DisplayName.SetDefault("Redeemer");
		}
		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bullet);
			AIType = ProjectileID.Bullet;
			Projectile.timeLeft = 60;
			Projectile.ignoreWater = true;
		}
		public override void AI()
        {
			if (Projectile.timeLeft < 12)
            {
				Projectile.alpha = 255 - Projectile.timeLeft * 255 / 12;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			damage = (int)(damage * Projectile.timeLeft / 60);
        }
    }
}
