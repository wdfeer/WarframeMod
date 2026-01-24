using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Consumables;

public class JahuCanticle : GrimoireUpgrade
{
    public const int ICHOR_DISTANCE = 50 * 16;
    public override GrimoireUpgradeType UpgradeType => GrimoireUpgradeType.JahuCanticle;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 2);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<JahuCanticlePlayer>().active = true;
    }
}

class JahuCanticlePlayer : ModPlayer
{
    public bool active;
    public override void ResetEffects()
        => active = false;

    public override float UseSpeedMultiplier(Item item)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.JahuCanticle)) return 1.15f;
        return 1f;
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (active && proj.ModProjectile is GrimoireProjectile or GrimoireAltProjectile)
        {
            target.GetGlobalNPC<GrimoireKillGlobalNPC>().Mark(Player, GrimoireUpgradeType.JahuCanticle);
        }
    }
}