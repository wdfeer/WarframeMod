using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneStrike : Arcane
{
    public const int CHANCE = 20;
    public const int SPEED_BUFF = 16;
    public const int BUFF_DURATION = 600;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneStrikePlayer>().enabled = true;
    }
}
class ArcaneStrikePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public int currentDefense = 0;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        if (Main.rand.Next(0, 100) < ArcaneStrike.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneStrikeBuff>(), ArcaneStrike.BUFF_DURATION);
    }
    public override void OnHitAnything(float x, float y, Entity victim)
    {
        ApplyBuff();
    }
}