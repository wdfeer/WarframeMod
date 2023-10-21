using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneGuardian : Arcane
{
    public const int DAMAGE_TO_DEFENSE_RATIO = 5;
    public const int BUFF_DURATION = 60 * 12;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<GuardianPlayer>().enabled = true;
    }
}
class GuardianPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public int currentDefense = 0;
    void ApplyBuff(int damage)
    {
        if (!enabled)
            return;
        currentDefense = (int)MathF.Ceiling((float)damage / ArcaneGuardian.DAMAGE_TO_DEFENSE_RATIO);
        Player.AddBuff(ModContent.BuffType<ArcaneGuardianBuff>(), ArcaneGuardian.BUFF_DURATION);
    }
    public override void OnHurt(Player.HurtInfo info)
    {
        ApplyBuff(info.Damage);
    }
}