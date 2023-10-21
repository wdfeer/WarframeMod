using Terraria.DataStructures;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneBodyguard : Arcane
{
    public const int CHANCE = 25;
    public const int DAMAGE_REDUCTION = 15;
    public const int BUFF_DURATION = 480;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<BodyguardPlayer>().enabled = true;
    }
}
class BodyguardPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    void ApplyBuff()
    {
        if (Main.rand.Next(0, 100) < ArcaneBodyguard.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneBodyguardBuff>(), ArcaneBodyguard.BUFF_DURATION);
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        bool IsSummon(DamageClass type)
            => type == DamageClass.Summon || type == DamageClass.SummonMeleeSpeed;
        if (enabled && IsSummon(proj.DamageType) && Player.HeldItem != null && IsSummon(Player.HeldItem.DamageType))
            ApplyBuff();
    }
    bool Active => Player.HasBuff(ModContent.BuffType<ArcaneBodyguardBuff>());
    float GetDamageMult()
    {
        return (100 - ArcaneBodyguard.DAMAGE_REDUCTION) / 100f;
    }
    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
        if (Active)
            modifiers.SourceDamage *= GetDamageMult();
    }
}