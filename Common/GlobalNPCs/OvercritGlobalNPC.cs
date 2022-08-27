using WarframeMod.Common.Players;

namespace WarframeMod.Common.GlobalNPCs;

internal class OvercritNPCVisuals : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public Color NoCritHitColor => CritsPlayer.GetCritColor(0);
    public Color DefaultCritColor => CritsPlayer.GetCritColor(1);
    public Color? thisCritColor;
    CombatText FindRecentCombatTextItem()
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
                        return Main.combatText[i];
                    }
                }
            }
        }
        return null;
    }
    CombatText FindRecentCombatTextProjectile()
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
                        return Main.combatText[i];
                    }
                }
            }
        }
        return null;
    }
    public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
    {
        CombatText recent = FindRecentCombatTextItem();
        if (recent == null) return;
        if (thisCritColor != null)
        {
            recent.color = (Color)thisCritColor;
            thisCritColor = null;
        }
        else if (crit) recent.color = DefaultCritColor;
        else recent.color = NoCritHitColor;
    }
    public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
    {
        CombatText recent = FindRecentCombatTextProjectile();
        if (recent == null) return;
        if (thisCritColor != null)
        {
            recent.color = (Color)thisCritColor;
            thisCritColor = null;
        }
        else if (crit) recent.color = DefaultCritColor;
        else recent.color = NoCritHitColor;
    }
}
