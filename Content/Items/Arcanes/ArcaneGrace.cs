using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneGrace : Arcane
{
    public const int CHANCE = 50;
    public const float LIFE_REGEN = 0.01f;
    public const int BUFF_DURATION = 420;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneGracePlayer>().enabled = true;
    }
}
class ArcaneGracePlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        if (Main.rand.Next(0, 100) < ArcaneGrace.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneGraceBuff>(), ArcaneGrace.BUFF_DURATION);
    }
    public override void OnHurt(Player.HurtInfo info)
    {
        ApplyBuff();
    }
    public bool Active => Player.HasBuff<ArcaneGraceBuff>();
    public float HealPerSecond => Player.statLifeMax2 * ArcaneGrace.LIFE_REGEN;
    const int HEAL_COOLDOWN = 60;
    int healTimer = 0;
    public override void UpdateLifeRegen()
    {
        if (Active)
        {
            healTimer++;
            if (healTimer > HEAL_COOLDOWN && Player.statLife < Player.statLifeMax2)
            {
                Player.Heal((int)MathF.Round(HealPerSecond));
                healTimer = 0;
            }
        }
    }
}