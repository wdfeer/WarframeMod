using WarframeMod.Common.Players;

namespace WarframeMod.Common.GlobalNPCs;

internal class OvercritNPCVisuals : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public Color NoCritHitColor => CritPlayer.GetCritColor(0);
    public Color DefaultCritColor => CritPlayer.GetCritColor(1);
    public int? nextCritLevel = null;
    int FindRecentCombatTextItem()
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
                        return i;
                    }
                }
            }
        }
        return -1;
    }
    int FindRecentCombatTextProjectile()
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
                        return i;
                    }
                }
            }
        }
        return -1;
    }
    public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
    {
        int recent = FindRecentCombatTextItem();
        if (recent == -1) return;
        SetCombatTextColor(recent, crit);
    }
    public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
    {
        var recent = FindRecentCombatTextProjectile();
        if (recent == -1) return;
        SetCombatTextColor(recent, crit);
    }
    void SetCombatTextColor(int combatText, bool crit)
    {
        CombatText text = Main.combatText[combatText];
        if (text == null || !text.active)
            return;
        if (nextCritLevel != null)
        {
            text.color = CritPlayer.GetCritColor(nextCritLevel.Value);
            nextCritLevel = null;
        }
        else if (crit)
        {
            text.color = DefaultCritColor;
        }
        else
        {
            text.color = NoCritHitColor;
        }
    }
}
