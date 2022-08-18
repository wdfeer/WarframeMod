using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WarframeMod;

public class WarframeClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
}