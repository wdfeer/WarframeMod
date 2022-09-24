namespace WarframeMod;
public partial class WarframeMod : Mod
{
    public static bool IsBossAlive()
        => Main.npc.Any(npc => npc.active && (npc.boss || npc.type == NPCID.EaterofWorldsBody));
}