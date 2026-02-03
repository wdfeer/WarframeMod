using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WarframeMod.Common.Configs;

public class WarframeServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [Header("Slots")]
    [DefaultValue(true)]
    public bool enableArcaneSlot;

    [Header("Stats")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool enableStatChanges;
    
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