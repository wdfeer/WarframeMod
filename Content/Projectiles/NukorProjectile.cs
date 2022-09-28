using Terraria.Audio;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;

public class NukorProjectile : BeamProjectile
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 2f;
    }
    public override int HitCooldown => 7;
    protected override int WeaponEnergyDustType => DustID.AncientLight;
    protected override Color WeaponEnergyDustColor => Color.Orange;
    protected override int Contact1DustType => DustID.OrangeTorch;
    protected override int Contact2DustType => DustID.RedTorch;
    public override DamageClass DamageClass => DamageClass.Magic;
    public override SoundStyle? ChargedSound => SoundID.DD2_BetsyWindAttack.ModifySoundStyle(volume: 0.15f, pitchVariance: 0.25f);
}
