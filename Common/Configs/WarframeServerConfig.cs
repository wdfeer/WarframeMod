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

    [DefaultValue(5)]
    [Range(0, 20)]
    [ReloadRequired]
    public int enemyMaxLifeIncreasePercent;

    [DefaultValue(5)]
    [Range(0, 20)]
    [ReloadRequired]
    public int enemyDefenseIncreasePercent;

    [DefaultValue(5)]
    [Range(0, 20)]
    [ReloadRequired]
    public int enemyDamageIncreasePercent;
}