using WarframeMod.Common.Players;

namespace WarframeMod.Common.GlobalNPCs;

internal class OvercritNPCVisuals : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public Color NoCritHitColor => CritPlayer.GetCritColor(0);
    public Color DefaultCritColor => CritPlayer.GetCritColor(1);
    public int? nextCritLevel = null;
    (CombatText, int)? FindRecentCombatTextItem()
    {
        for (int i = 99; i >= 0; i--)
        {
            CombatText ctToCheck = Main.combatText[i];
            if (ctToCheck.lifeTime == 60 || ctToCheck.lifeTime == 120 || ctToCheck.dot && ctToCheck.lifeTime == 40)
            {
                if (ctToCheck.alpha == 1f)
                {
                    if (ctToCheck.color == CombatText.DamagedHostile || ctToCheck.color == CombatText.DamagedHostileCrit)
                    {
                        return (Main.combatText[i], i);
                    }
                }
            }
        }
        return null;
    }
    (CombatText, int)? FindRecentCombatTextProjectile()
    {
        for (int i = 99; i >= 0; i--)
        {
            CombatText ctToCheck = Main.combatText[i];
            if (ctToCheck.lifeTime == 60 || ctToCheck.lifeTime == 120)
            {
                if (ctToCheck.alpha == 1f)
                {
                    if (ctToCheck.color == CombatText.DamagedHostile || ctToCheck.color == CombatText.DamagedHostileCrit)
                    {
                        return (Main.combatText[i], i);
                    }
                }
            }
        }
        return null;
    }
    public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
    {
        var recent = FindRecentCombatTextItem();
        if (recent == null || recent.Value.Item1 == null) return;
        CombatText combatText = recent.Value.Item1;

        if (nextCritLevel != null)
        {
            combatText.color = CritPlayer.GetCritColor(nextCritLevel.Value);
            NetUpdateCombatTextColor(recent.Value.Item2);
            nextCritLevel = null;
        }
        else if (crit) combatText.color = DefaultCritColor;
        else combatText.color = NoCritHitColor;
        NetUpdateCombatTextColor(recent.Value.Item2);
    }
    public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
    {
        var recent = FindRecentCombatTextProjectile();
        if (recent == null || recent.Value.Item1 == null) return;
        CombatText combatText = recent.Value.Item1;

        if (combatText == null) return;
        if (nextCritLevel != null)
        {
            combatText.color = CritPlayer.GetCritColor(nextCritLevel.Value);
            NetUpdateCombatTextColor(recent.Value.Item2);
            nextCritLevel = null;
        }
        else if (crit) combatText.color = DefaultCritColor;
        else combatText.color = NoCritHitColor;
    }
    void NetUpdateCombatTextColor(int combatText)
    {
        if (Main.netMode is NetmodeID.SinglePlayer or NetmodeID.Server)
            return;
        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)WarframeMod.MessageType.CombatTextCritLevel);
        packet.Write((byte)combatText);
        int crit = nextCritLevel == null ? 0 : (int)nextCritLevel;
        packet.Write((byte)crit);
        packet.Send();
    }
}
