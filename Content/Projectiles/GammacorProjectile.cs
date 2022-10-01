using Terraria.Audio;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class GammacorProjectile : BeamProjectile
{
    public override string Texture => "WarframeMod/Content/Projectiles/FluxRifleProjectile";
    protected override float MinDistance => 45f;
    public override int HitCooldown => 5;
    protected override int WeaponEnergyDustType => DustID.AncientLight;
    protected override Color WeaponEnergyDustColor => new Color(1f, 0.9f, 1f);
    protected override int Contact1DustType => -1;
    protected override int Contact2DustType => -1;
    protected override float ExtraDistanceOnLastNPCCollision => 7.5f;
    protected override Action OnDetectHitNPC => () =>
    {
        for (int i = 0; i < 4; i++)
        {
            Dust d = Dust.NewDustDirect(Owner.Center + Projectile.velocity * Distance, 0, 0, DustID.AncientLight);
            d.noGravity = true;
            d.velocity = Vector2.Normalize(Projectile.velocity) * 9 + Main.rand.NextVector2CircularEdge(2, 2);
        }
    };
    public override DamageClass DamageClass => DamageClass.Magic;
    public override SoundStyle? ChargedSound => SoundID.DD2_BetsyWindAttack.ModifySoundStyle(volume: 0.07f, pitchVariance: 0.1f);
}
