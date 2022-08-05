using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace WarframeMod
{
    internal class FireRatePlayer : ModPlayer
    {
        public float FireRateMultiplier { get; set; }
        public override void ResetEffects()
        {
            FireRateMultiplier = 1;
        }
    }
    internal class FireRateGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            return player.GetModPlayer<FireRatePlayer>().FireRateMultiplier;
        }
    }
}
