using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneEruption : Arcane
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RANGE / 16);
    public const int RANGE = 50 * 16;
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneEruptionPlayer>().enabled = true;
    }
}
class ArcaneEruptionPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public void OnPickupStarWhenEnabled()
    {
        Projectile.NewProjectileDirect(Player.GetSource_FromThis(),
                                       Player.Center,
                                       Vector2.Zero,
                                       ModContent.ProjectileType<ArcaneEruptionProjectile>(),
                                       Player.statDefense,
                                       5f,
                                       Player.whoAmI);
    }
    public readonly int[] starTypes = [ItemID.Star, ItemID.SoulCake, ItemID.SugarPlum];
    public override bool OnPickup(Item item)
    {
        if (enabled && starTypes.Contains(item.type))
            OnPickupStarWhenEnabled();
        return base.OnPickup(item);
    }
}