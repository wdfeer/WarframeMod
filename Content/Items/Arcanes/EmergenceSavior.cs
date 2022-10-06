using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class EmergenceSavior : Arcane
{
    public const float LIFE_RESTORATION = 0.25f;
    public const int EXTRA_IFRAMES = 120;
    public const int COOLDOWN = 60 * 60;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"Upon taking lethal damage: become invulnerable for {EXTRA_IFRAMES / 60} seconds and restore {(int)(LIFE_RESTORATION * 100f)}% life\nCooldown: {COOLDOWN / 60} seconds");
    }
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<SaviourPlayer>().enabled = true;
    }
}
class SaviourPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
    {
        if (enabled && !Player.HasBuff<EmergenceSaviorBuff>())
        {
            Player.AddBuff(ModContent.BuffType<EmergenceSaviorBuff>(), EmergenceSavior.COOLDOWN);
            Player.AddImmuneTime(ImmunityCooldownID.General, EmergenceSavior.EXTRA_IFRAMES);
            Player.Heal((int)(Player.statLifeMax2 * EmergenceSavior.LIFE_RESTORATION));
            SoundEngine.PlaySound(SoundID.Item119, Player.Center);
            return false;
        }
        return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
    }
    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        if (!enabled && Player.HasBuff<EmergenceSaviorBuff>())
        {
            damage *= 10;
        }
        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    }
}