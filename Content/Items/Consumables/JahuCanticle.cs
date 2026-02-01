using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Consumables;

public class JahuCanticle : GrimoireUpgrade
{
    public override void SetStaticDefaults()
    {
        OnKillGlobalNPC.RegisterOnKillEvent(hit =>
                (hit.projectileType == ModContent.ProjectileType<GrimoireProjectile>() ||
                 hit.projectileType == ModContent.ProjectileType<GrimoireAltProjectile>()) && Grimoire
                    .GetPlayerGrimoire(hit.player).upgrades.Contains(GrimoireUpgradeType.JahuCanticle),
            hit =>
            {
                foreach (NPC other in Main.npc.Where(other =>
                             !other.friendly &&
                             hit.target.Distance(other.position) < JahuCanticle.ICHOR_DISTANCE))
                {
                    other.AddBuff(BuffID.Ichor, 10 * 60);
                }
            });
    }

    public const int ICHOR_DISTANCE = 50 * 16;
    public override GrimoireUpgradeType UpgradeType => GrimoireUpgradeType.JahuCanticle;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 2);
    }
}

class JahuCanticlePlayer : ModPlayer
{
    private bool active;

    public override void ResetEffects()
    {
        var grimoire = Grimoire.GetPlayerGrimoire(Player);
        active = grimoire != null && grimoire.HasUpgrade(GrimoireUpgradeType.JahuCanticle);
    }

    public override float UseSpeedMultiplier(Item item)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.JahuCanticle)) return 1.15f;
        return 1f;
    }
}