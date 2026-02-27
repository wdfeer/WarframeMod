using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneCamisado : Arcane
{
    public const int MAGIC_DAMAGE_PER_PROC = 3;
    public const int MAGIC_DAMAGE_MAX = 39;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MAGIC_DAMAGE_PER_PROC, MAGIC_DAMAGE_MAX);
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneCamisadoPlayer>().enabled = true;
    }
}

class ArcaneCamisadoPlayer : ModPlayer
{
    public bool enabled;
    public int stacks;
    private const int MaxStacks = ArcaneCamisado.MAGIC_DAMAGE_MAX / ArcaneCamisado.MAGIC_DAMAGE_PER_PROC;

    public override void ResetEffects()
    {
        if (!enabled || !Player.HasBuff<ArcaneCamisadoBuff>())
            stacks = 0;
        enabled = false;
    }

    public override void PostUpdateEquips()
    {
        if (enabled)
            Player.GetDamage<MagicDamageClass>() += stacks * ArcaneCamisado.MAGIC_DAMAGE_PER_PROC / 100f;
    }

    public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage,
        float knockback)
    {
        if (enabled && item.DamageType.CountsAsClass<MagicDamageClass>())
        {
            stacks = 0;
            Player.ClearBuff(ModContent.BuffType<ArcaneCamisadoBuff>());
        }
        
        return base.Shoot(item, source, position, velocity, type, damage, knockback);
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        // does not refresh duration if at max stacks
        if (enabled && stacks < MaxStacks && proj.DamageType.CountsAsClass<SummonDamageClass>())
        {
            Player.AddBuff(ModContent.BuffType<ArcaneCamisadoBuff>(), 10 * 60);
            stacks++;
        }
    }
}