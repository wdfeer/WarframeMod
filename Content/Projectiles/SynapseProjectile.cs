using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using Terraria.GameContent;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Projectiles;

public class SynapseProjectile : BeamProjectile
{
    public override string Texture => "WarframeMod/Content/Projectiles/NukorProjectile";
    protected override float MaxCharge => 33f;
    protected override float MoveDistance => 75f;
    public override int HitCooldown => 5;
    protected override int WeaponEnergyDustType => DustID.AncientLight;
    protected override Color WeaponEnergyDustColor => Color.Orange;
    protected override int Contact1DustType => DustID.OrangeTorch;
    protected override int Contact2DustType => DustID.RedTorch;
    public override DamageClass DamageClass => DamageClass.Magic;
}
