using Terraria.Localization;
using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Consumables;

public class LohkCanticle : GrimoireUpgrade
{
    public const int FIRE_RATE_INCREASE_PERCENT = 15;
    public const int BUFF_TIME_SECONDS = 15;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FIRE_RATE_INCREASE_PERCENT, BUFF_TIME_SECONDS);
    public override GrimoireUpgradeType UpgradeType => GrimoireUpgradeType.LohkCanticle;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 2);
    }
}

class LohkCanticlePlayer : ModPlayer
{
    private bool active;

    public override void ResetEffects()
    {
        var grimoire = Grimoire.GetPlayerGrimoire(Player);
        active = grimoire != null && grimoire.HasUpgrade(GrimoireUpgradeType.LohkCanticle);
    }

    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.LohkCanticle))
            damage.Base += 30;
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (active && proj.ModProjectile is GrimoireProjectile or GrimoireAltProjectile)
        {
            target.GetGlobalNPC<GrimoireKillGlobalNPC>().Mark(Player, GrimoireUpgradeType.LohkCanticle);
        }
    }
}