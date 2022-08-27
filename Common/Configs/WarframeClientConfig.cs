using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WarframeMod.Common.Configs;

public class WarframeClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    [Header("Non-critical hit color")]
    [DefaultValue(typeof(Color), "245, 245, 245, 255")]
    public Color noCritHitColor;
    [Header("Tier 1 critical hit color")]
    [DefaultValue(typeof(Color), "255, 255, 0, 255")]
    public Color tier1CritColor;
    [Header("Tier 2 critical hit color")]
    [DefaultValue(typeof(Color), "255, 166, 0, 255")]
    public Color tier2CritColor;
    [Header("Tier 3+ critical hit color")]
    [DefaultValue(typeof(Color), "255, 0, 0, 255")]
    public Color maxCritColor;
}