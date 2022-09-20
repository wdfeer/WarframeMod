using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcanePistoleer : Arcane
{
    public const int CHANCE = 33;
    public const float AMMO_DAMAGE_INCREASE = 0.33f;
    public const int BUFF_DURATION = 360;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On critical hit: {CHANCE}% chance for +{(int)(AMMO_DAMAGE_INCREASE * 100)}% damage from ammo for {BUFF_DURATION / 60} seconds");
    }
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcanePistoleerPlayer>().enabled = true;
    }
}
class ArcanePistoleerPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    void ApplyBuff(Projectile proj, bool crit)
    {
        if (enabled && crit && Main.rand.Next(0, 100) < ArcanePistoleer.CHANCE)
            Player.AddBuff(ModContent.BuffType<ArcanePistoleerBuff>(), ArcanePistoleer.BUFF_DURATION);
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        ApplyBuff(proj, crit);
    }
    public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
    {
        ApplyBuff(proj, crit);
    }
    public bool Active => Player.HasBuff<ArcanePistoleerBuff>();
    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (!Active)
            return;
        if (item.useAmmo > 0)
        {
            damage.Base += Player.ChooseAmmo(item).damage * ArcanePistoleer.AMMO_DAMAGE_INCREASE;
        }
    }
}