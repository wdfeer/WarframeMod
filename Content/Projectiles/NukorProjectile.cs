using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using Terraria.GameContent;
using WarframeMod.Common.GlobalProjectiles;

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
}
