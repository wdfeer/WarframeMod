using Terraria.Localization;
using WarframeMod.Content.Buffs;
using WarframeMod.Content.Items.Weapons;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Consumables;

public class LohkCanticle : GrimoireUpgrade
{
    public const int FIRE_RATE_INCREASE_PERCENT = 15;
    public const int BUFF_TIME = 15 * 60;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FIRE_RATE_INCREASE_PERCENT, BUFF_TIME);
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

    public override void ResetEffects() =>
        active = Grimoire.GetPlayerGrimoire(Player).HasUpgrade(GrimoireUpgradeType.LohkCanticle);

    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (item.ModItem is Grimoire grimoire && grimoire.HasUpgrade(GrimoireUpgradeType.LohkCanticle))
            damage.Base += 20;
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (active && proj.ModProjectile is GrimoireProjectile or GrimoireAltProjectile)
        {
            target.GetGlobalNPC<LohkCanticleGlobalNPC>().markedByTeam = Player.team;
        }
    }
}
class LohkCanticleGlobalNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public int markedByTeam = -1;
    public override bool PreKill(NPC npc)
    {
        if (markedByTeam != -1)
        {
            foreach (var player in Main.player.Where(it => it.active && it.team == markedByTeam))
            {
                player.AddBuff(ModContent.BuffType<LohkCanticleBuff>(), LohkCanticle.BUFF_TIME,
                    Main.LocalPlayer == player);
            }
        }

        return base.PreKill(npc);
    }
}