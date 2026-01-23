using Terraria.Localization;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class MoltVigor : Arcane
{
    public const int OTHER_WEAPON_DAMAGE_INCREASE_PERCENT = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(OTHER_WEAPON_DAMAGE_INCREASE_PERCENT);
    public const int BUFF_DURATION = 15 * 60;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<MoltVigorPlayer>().enabled = true;
    }
}
class MoltVigorPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    private int lastWeaponType;
    public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type,
        ref int damage,
        ref float knockback)
    {
        if (enabled && item.DamageType == DamageClass.Magic && item.type != lastWeaponType)
        {
            Player.AddBuff(ModContent.BuffType<MoltVigorBuff>(), MoltVigor.BUFF_DURATION);
            lastWeaponType = item.type;
        }
    }
    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (enabled && item.DamageType == DamageClass.Magic && item.type != lastWeaponType &&
            Player.HasBuff<MoltVigorBuff>()) damage += MoltVigor.OTHER_WEAPON_DAMAGE_INCREASE_PERCENT / 100f;
    }
}