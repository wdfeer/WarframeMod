using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeMod.Content.Projectiles;
internal class TenetArcaPlasmorProjectile : ArcaPlasmorProjectile
{
    public override bool Tenet => true;
    public override string Texture => "WarframeMod/Content/Projectiles/ArcaPlasmorProjectile";
}
