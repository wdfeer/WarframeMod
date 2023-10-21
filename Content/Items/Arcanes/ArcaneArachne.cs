using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneArachne : Arcane
{
    public const int DAMAGE_BUFF = 25;
    public const int COOLDOWN_DURATION = 60 * 30;
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
    public override void OnHurt(Player.HurtInfo info)
    {
        if (enabled)
            Player.AddBuff(ModContent.BuffType<ArcaneArachneBuff>(), ArcaneArachne.COOLDOWN_DURATION);
    }
}