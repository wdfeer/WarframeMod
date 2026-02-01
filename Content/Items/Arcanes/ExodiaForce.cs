using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Arcanes;

public class ExodiaForce : Arcane
{
    public const int COOLDOWN_SECONDS = 4;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(COOLDOWN_SECONDS);

    public override void UpdateArcane(Player player)
    {
        var modPlayer = player.GetModPlayer<ExodiaForcePlayer>();
        modPlayer.enabled = true;
        modPlayer.timer--;
    }
}

class ExodiaForcePlayer : ModPlayer
{
    public bool enabled;
    public int timer;
    public override void ResetEffects() => enabled = false;

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (enabled && timer <= 0)
        {
            Projectile.NewProjectileDirect(Player.GetSource_ItemUse(item),
                target.Center,
                Vector2.Zero,
                ModContent.ProjectileType<ExodiaForceProjectile>(),
                damageDone * 2,
                0f
            );
            timer = ExodiaForce.COOLDOWN_SECONDS * 60;
        }
    }
}