using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcanePersistence : Arcane
{
    public const int HP_PERCENT = 50;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(HP_PERCENT);
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcanePersistencePlayer>().enabled = true;
    }
}
class ArcanePersistencePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;

    public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
        if (enabled)
        {
            modifiers.SetMaxDamage((int)(Player.statLifeMax2 * ArcanePersistence.HP_PERCENT / 100f));
        }
    }

    public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
        if (enabled)
        {
            modifiers.SetMaxDamage((int)(Player.statLifeMax2 * ArcanePersistence.HP_PERCENT / 100f));
        }
    }
}