using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WarframeMod.Common.Configs;

public class WarframeServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [Header("Stats")]
    [DefaultValue(10)]
    [Range(0, 20)]
    [ReloadRequired]
    public int vanillaCritIncrease;
}