using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class GrimoireProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;
    public override int ExplosionWidth => 64;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.AmethystBolt);
        AIType = ProjectileID.AmethystBolt;
        base.SetDefaults();
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(Grimoire.ELECTRO_CHANCE);
    }

    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCircleFromCenter(8,
            Projectile.Center,
            ExplosionWidth / 2,
            DustID.Electric,
            2f);
    }

    public override void ExplosionSound()
    {
        // SoundEngine.PlaySound(SoundID.Dig);
    }
}