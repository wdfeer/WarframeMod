using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneAvenger : Arcane
{
    public const int DAMAGE_TO_CRIT_RATIO = 2;
    public const int BUFF_DURATION = 720;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<AvengerPlayer>().enabled = true;
    }
}
class AvengerPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public int currentCritChance = 0;
    void ApplyBuff(int damage)
    {
        if (!enabled)
            return;
        currentCritChance = (int)MathF.Ceiling((float)damage / ArcaneAvenger.DAMAGE_TO_CRIT_RATIO);
        Player.AddBuff(ModContent.BuffType<ArcaneAvengerBuff>(), ArcaneAvenger.BUFF_DURATION);
    }
    public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
    {
        ApplyBuff((int)damage);
    }
}