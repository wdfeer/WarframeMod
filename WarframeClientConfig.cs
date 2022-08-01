using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace WarframeMod
{
    public class WarframeClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Range(0f, 3f)]
        [Increment(.2f)]
        [DrawTicks]
        [DefaultValue(1f)]
        public float OvercritVisualIntensity;
    }
}