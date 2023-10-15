using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcanePistoleer : Arcane
{
    public const int CHANCE = 33;
    public const float AMMO_DAMAGE_INCREASE = 0.33f;
    public const int BUFF_DURATION = 360;
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
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        ApplyBuff(proj, hit.Crit);
    } // No PVP support yet
    public static bool IsBuffed(Player player) => player.HasBuff<ArcanePistoleerBuff>();
    public bool IsBuffed() => IsBuffed(Player);
    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (!IsBuffed())
            return;
        if (item.ammo > 0)
        {
            damage *= 1 + ArcanePistoleer.AMMO_DAMAGE_INCREASE; //only visual increase
        }
    }
}
class ArcanePistoleerGlobalItem : GlobalItem
{
    public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
    {
        if (ArcanePistoleerPlayer.IsBuffed(player) && ammo != null)
        {
            damage.Base += ammo.damage * ArcanePistoleer.AMMO_DAMAGE_INCREASE;
        }
    }
}