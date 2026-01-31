using Terraria.Localization;
using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Accessories;
public class AnabolicPollination : ModItem
{
    public const int POISON_CHANCE_PER_MINION_PERCENT = 3;
    public const int EXTRA_POISON_DPS_PER_MINION = 35;
    public const int POISON_DURATION = 300;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(POISON_CHANCE_PER_MINION_PERCENT, EXTRA_POISON_DPS_PER_MINION);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.buyPrice(gold: 30);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AnabolicPollinationPlayer>().enabled = true;
    }
}

class AnabolicPollinationPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;

    private float PoisonChance => (Player.maxMinions - 1) * AnabolicPollination.POISON_CHANCE_PER_MINION_PERCENT / 100f;
    private float PoisonDPS => (Player.maxMinions - 1) * AnabolicPollination.EXTRA_POISON_DPS_PER_MINION;
    void OnHitNPCWithSomething(NPC target)
    {
        if (!enabled || PoisonChance <= 0f)
            return;
        if (Main.rand.NextFloat() < PoisonChance)
        {
            target.AddBuff(BuffID.Poisoned, AnabolicPollination.POISON_DURATION);
            target.GetGlobalNPC<DebuffDamageGlobalNPC>()
                .AddBuffDamage(DebuffDamageGlobalNPC.SourceId.AnabolicPollination,
                    BuffID.Poisoned,
                    (int)PoisonDPS);
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        OnHitNPCWithSomething(target);
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        OnHitNPCWithSomething(target);
    }
}