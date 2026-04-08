using WarframeMod.Common.Configs;

namespace WarframeMod.Common.GlobalNPCs;

public class EximusGlobalNPC : GlobalNPC
{
    public override void SetDefaults(NPC entity)
    {
        base.SetDefaults(entity);
        if (!entity.boss && Main.rand.Next(100) < ModContent.GetInstance<WarframeServerConfig>().eximusChancePercent) {
            // TODO: make `entity` an eximus unit
        }
    }
}