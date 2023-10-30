using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WarframeMod.Common.Configs;

public class WarframeClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header("Config.hitcolors")]
    [DefaultValue(typeof(Color), "245, 245, 245, 255")]
    public Color noCritHitColor;
    [DefaultValue(typeof(Color), "255, 255, 0, 255")]
    public Color tier1CritColor;
    [DefaultValue(typeof(Color), "255, 166, 0, 255")]
    public Color tier2CritColor;
    [DefaultValue(typeof(Color), "255, 0, 0, 255")]
    public Color maxCritColor;
}