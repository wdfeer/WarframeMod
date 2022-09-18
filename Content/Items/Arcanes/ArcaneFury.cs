using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneFury : Arcane
{
    public const int CHANCE = 20;
    public const int DAMAGE_BUFF = 14;
    public const int BUFF_DURATION = 960;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On Critical hit: {CHANCE}% chance for +{DAMAGE_BUFF}% melee Damage for {BUFF_DURATION / 60} seconds");
    }
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneFuryPlayer>().enabled = true;
    }
}
class ArcaneFuryPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public int currentDefense = 0;
    void ApplyBuff()
    {
        if (!enabled)
            return;
        if (Main.rand.Next(0, 100) < ArcaneFury.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcaneFuryBuff>(), ArcaneFury.BUFF_DURATION);
    }
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        if (crit) ApplyBuff();
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (crit) ApplyBuff();
    }
}