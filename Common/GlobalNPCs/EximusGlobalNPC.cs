using WarframeMod.Common.Configs;

namespace WarframeMod.Common.GlobalNPCs;

public class EximusGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    
    public enum EximusType
    {
        None = 0,
        Shock,
        // TODO: implement more eximus types
    }
    public EximusType eximus;

    private const float lifeMult = 2;
    
    public override void SetDefaults(NPC entity)
    {
        base.SetDefaults(entity);
        if (!entity.boss && Main.rand.Next(100) < ModContent.GetInstance<WarframeServerConfig>().eximusChancePercent)
        {
            var allTypes = Enum.GetValues<EximusType>();
            eximus = allTypes[Main.rand.Next(allTypes.Length - 1) + 1]; // without None

            entity.lifeMax = (int)(entity.lifeMax * lifeMult);
            entity.life = entity.lifeMax;
            
            // TODO: implement overguard
        }
    }
}