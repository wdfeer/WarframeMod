using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using Terraria.GameContent;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class FluxRifleProjectile : BeamProjectile
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.ArmorPenetration = FluxRifle.DEFENSE_PENETRATION;
    }
    public override int HitCooldown => 5;
    protected override int WeaponEnergyDustType => DustID.AncientLight;
    protected override Color WeaponEnergyDustColor => Color.LightCyan;
    protected override int Contact1DustType => -1;
    protected override int Contact2DustType => -1;
    protected override float ExtraDistanceOnLastNPCCollision => 10f;
    protected override Action OnDetectHitNPC => () =>
    {
        for (int i = 0; i < 5; i++)
        {
            Dust d = Dust.NewDustDirect(Owner.Center + Projectile.velocity * Distance, 0, 0, DustID.AncientLight);
            d.noGravity = true;
            d.velocity = Vector2.Normalize(Projectile.velocity) * 12 + Main.rand.NextVector2CircularEdge(5, 5);
        }
    };
}
