using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneVictory : Arcane
{
    public const int CHANCE = 20;
    public const float LIFE_REGEN = 0.0045f;
    public const int BUFF_DURATION = 600;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneVictoryPlayer>().enabled = true;
    }
}
class ArcaneVictoryPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        if (Main.rand.Next(0, 100) < ArcaneVictory.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneVictoryBuff>(), ArcaneVictory.BUFF_DURATION);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit) ApplyBuff();
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit) ApplyBuff();
    }
    public bool Active => Player.HasBuff<ArcaneVictoryBuff>();
    public float HealPerSecond => Player.statLifeMax2 * ArcaneVictory.LIFE_REGEN;
    const int healCooldown = 60;
    int healTimer = 0;
    public override void UpdateLifeRegen()
    {
        if (Active)
        {
            healTimer++;
            if (healTimer > healCooldown && Player.statLife < Player.statLifeMax2)
            {
                int floored = (int)HealPerSecond;
                int extraRandom = (Main.rand.NextFloat() < HealPerSecond % 1 ? 1 : 0);
                Player.Heal(floored + extraRandom);
                healTimer = 0;
            }
        }
    }
}