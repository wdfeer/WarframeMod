using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class GrimoireProjectile : ExplosiveProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.AmethystBolt;
    public override int ExplosionWidth => 80;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.AmethystBolt);
        AIType = ProjectileID.AmethystBolt;
        base.SetDefaults();
        Projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(Grimoire.ELECTRO_CHANCE);
        Projectile.extraUpdates = 1;
    }

    public override void ExplosionDusts()
    {
        DustHelper.NewDustsCircleEdge(7, Projectile.Center, ExplosionWidth / 2, DustID.GemAmethyst);
        DustHelper.NewDustsCircleFromCenter(3,
            Projectile.Center,
            ExplosionWidth / 2f,
            DustID.Electric,
            2f);
    }
}