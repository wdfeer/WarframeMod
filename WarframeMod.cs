namespace WarframeMod;
public partial class WarframeMod : Mod
{
    public static WarframeMod instance;
    public override void Load()
    {
        instance = this;
    }
    public static bool IsABossAlive()
        => Main.npc.Any(npc => npc.active && (npc.boss || npc.type == NPCID.EaterofWorldsBody));
}