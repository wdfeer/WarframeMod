using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneArachne : Arcane
{
    public const int DAMAGE_BUFF = 25;
    public const int COOLDOWN_DURATION = 1800;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{DAMAGE_BUFF}% damage\nWhen damaged: disable the above effect for {COOLDOWN_DURATION / 60} seconds");
    }
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArachnePlayer>().enabled = true;
        player.GetDamage(DamageClass.Generic) += DAMAGE_BUFF / 100f;
    }
}
class ArachnePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
    {
        if (enabled)
            Player.AddBuff(ModContent.BuffType<ArcaneArachneBuff>(), ArcaneArachne.COOLDOWN_DURATION);
    }
}