using WarframeMod.Common.Configs;

namespace WarframeMod.Common.GlobalNPCs;

public class EximusGlobalNPC : GlobalNPC
{
    public enum EximusType
    {
        None = 0,
        Shock,
        // TODO: implement more eximus types
    }
    public override bool InstancePerEntity => true;
    public EximusType eximus;
    public override void SetDefaults(NPC entity)
    {
        base.SetDefaults(entity);
        if (!entity.boss && Main.rand.Next(100) < ModContent.GetInstance<WarframeServerConfig>().eximusChancePercent)
        {
            var allTypes = Enum.GetValues<EximusType>();
            eximus = allTypes[Main.rand.Next(allTypes.Length - 1) + 1]; // without None
            
            // TODO: increase enemy hp
        }
    }
}