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
	public class NukorProjectile : ModProjectile
	{
		public const float critDmgMult = 2f;
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.EmeraldBolt;
        public override void SetStaticDefaults() {
			DisplayName.SetDefault("Nukor");
		}
		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.TopazBolt);
			AIType = ProjectileID.TopazBolt;
			Projectile.penetrate = 1;
			Projectile.extraUpdates = 10;
			Projectile.timeLeft = 45;
			Projectile.ignoreWater = true;
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if (crit)
				damage = (int)(damage * critDmgMult);
        }
    }
}
