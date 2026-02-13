using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public abstract class MotusAccessory : ModItem
{
    public const int KNOCKBACK_REDUCTION = 33;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<MotusPlayer>().motusCount++;
    }
}

public class MotusPlayer : ModPlayer
{
    public int motusCount;
    public float KnockbackMult => MathF.Max(0f, 1f - (motusCount * MotusAccessory.KNOCKBACK_REDUCTION / 100f));

    public override void ResetEffects()
        => motusCount = 0;

    public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
        if (Player.GetModPlayer<AirbornePlayer>().Airborne)
            modifiers.Knockback *= KnockbackMult;
    }

    public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
        if (Player.GetModPlayer<AirbornePlayer>().Airborne)
            modifiers.Knockback *= KnockbackMult;
    }
}