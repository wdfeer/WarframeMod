using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace WarframeMod.Global
{
    internal class CritGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public float CritMultiplier { get; set; } = 1f;
        public int critLevel;
    }
}
