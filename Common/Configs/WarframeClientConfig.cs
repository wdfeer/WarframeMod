using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WarframeMod.Common.Configs;

public class WarframeClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header("Config.noCritHitColor")]
    [DefaultValue(typeof(Color), "245, 245, 245, 255")]
    public Color noCritHitColor;
    [Header("Config.tier1CritColor")]
    [DefaultValue(typeof(Color), "255, 255, 0, 255")]
    public Color tier1CritColor;
    [Header("Config.tier2CritColor")]
    [DefaultValue(typeof(Color), "255, 166, 0, 255")]
    public Color tier2CritColor;
    [Header("Config.maxCritColor")]
    [DefaultValue(typeof(Color), "255, 0, 0, 255")]
    public Color maxCritColor;
}