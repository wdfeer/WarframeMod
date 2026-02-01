using Terraria.Localization;
using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Consumables;

public class LohkCanticle : GrimoireUpgrade
{
    public override void SetStaticDefaults()
    {
        OnKillGlobalNPC.RegisterOnKillEvent(hit =>
                (hit.projectileType == ModContent.ProjectileType<GrimoireProjectile>() ||
                 hit.projectileType == ModContent.ProjectileType<GrimoireAltProjectile>()) && Grimoire
                    .GetPlayerGrimoire(hit.player).upgrades.Contains(GrimoireUpgradeType.LohkCanticle),
            hit =>
            {
                if (!hit.player.active) return;
                
                int team = hit.player.team;
                foreach (var player in Main.player.Where(it => it.active && it.team == team))
                {
                    player.AddBuff(ModContent.BuffType<LohkCanticleBuff>(), BUFF_TIME_SECONDS * 60,
                        Main.LocalPlayer == player);
                }
            });
    }

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
}