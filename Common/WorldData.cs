using Terraria.ModLoader.IO;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Common;

public class WorldData : ModSystem
{
    public override void LoadWorldData(TagCompound tag)
    {
        Grimoire.vomeInvocationActive = (Byte)tag["VomeInvocationActive"] != 0;
    }
    public override void SaveWorldData(TagCompound tag)
    {
        tag["VomeInvocationActive"] = Grimoire.vomeInvocationActive;
    }
}