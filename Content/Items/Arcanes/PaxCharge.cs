using Terraria.Localization;
using WarframeMod.Common.Players;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Arcanes;

public class PaxCharge : Arcane
{
    public const int MAGIC_DAMAGE_INCREASE = 10;
    public const int MANA_REGEN = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MAGIC_DAMAGE_INCREASE);
    public override void UpdateArcane(Player player)
    {
        if (!player.HasBuff(BuffID.ManaSickness))
        {
            player.GetDamage<MagicDamageClass>() += MAGIC_DAMAGE_INCREASE / 100f;
            
            player.manaRegenBuff = true;
            player.manaRegenBonus += MANA_REGEN;
        }
    }
}