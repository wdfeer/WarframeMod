using Terraria.Localization;

namespace WarframeMod.Content.Items.Arcanes;

public class ExodiaValor : Arcane
{
    public const int TRUE_MELEE_DAMAGE_INCREASE = 25;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(TRUE_MELEE_DAMAGE_INCREASE);

    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ExodiaValorPlayer>().enabled = true;
    }
}

class ExodiaValorPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;

    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (enabled && MathF.Abs(Player.velocity.Y) > 16f / 60f && !item.noMelee && item.shoot == ProjectileID.None)
        {
            damage += ExodiaValor.TRUE_MELEE_DAMAGE_INCREASE / 100f;
        }
    }
}