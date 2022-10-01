using Terraria.Audio;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class SpectraProjectile : BeamProjectile
{
    public override string Texture => "WarframeMod/Content/Projectiles/FluxRifleProjectile";
    protected override float MinDistance => 48f;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.ArmorPenetration = Spectra.DEFENSE_PENETRATION;
    }
    public override int HitCooldown => 5;
    protected override int WeaponEnergyDustType => DustID.AncientLight;
    protected override Color WeaponEnergyDustColor => Color.White;
    protected override int Contact1DustType => -1;
    protected override int Contact2DustType => -1;
    protected override float ExtraDistanceOnLastNPCCollision => 10f;
    protected override Action OnDetectHitNPC => () =>
    {
        for (int i = 0; i < 3; i++)
        {
            Dust d = Dust.NewDustDirect(Owner.Center + Projectile.velocity * Distance, 0, 0, DustID.AncientLight);
            d.alpha = 128;
            d.noGravity = true;
            d.velocity = Vector2.Normalize(Projectile.velocity) * 4 + Main.rand.NextVector2CircularEdge(5, 5);
        }
    };
    public override DamageClass DamageClass => DamageClass.Magic;
    public override SoundStyle? ChargedSound => SoundID.DD2_BetsyWindAttack.ModifySoundStyle(volume: 0.07f, pitchVariance: 0.09f);
}
