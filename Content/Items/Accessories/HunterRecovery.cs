using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Common.Players;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class HunterRecovery : HunterAccessory
{
    public const int HEAL_TIMES_PER_SECOND = 2;
    public override string DefaultTooltip => $"Summon hits heal 1 life, up to {HEAL_TIMES_PER_SECOND} times per second\nHeal amount scales with your max life and your minion slots";
    int oldHealAmount = 1;
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var t0 = tooltips.Find(t => t.Name == "Tooltip0");
        if (t0 != null)
            t0.Text = $"Summon hits heal {oldHealAmount} life, up to {HEAL_TIMES_PER_SECOND} times per second";
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 5);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        var modPl = player.GetModPlayer<HunterRecoveryPlayer>();
        modPl.enabled = true;
        oldHealAmount = modPl.oldHealAmount;
    }
}
class HunterRecoveryPlayer : ModPlayer
{
    public bool enabled = false;
    public override void ResetEffects()
        => enabled = false;
    const int HEAL_COOLDOWN = 60 / HunterRecovery.HEAL_TIMES_PER_SECOND;
    int healTimer = 0;
    public int oldHealAmount = 1;
    public int GetHealAmount()
    {
        int calculated = (int)(Player.statLifeMax2 / 200f * (Player.maxMinions > 6.5f ? 1f : Player.maxMinions / 6.5f));
        if (calculated > 0)
            return calculated;
        return 1;
    }
    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (enabled && proj.DamageType == DamageClass.Summon && healTimer > HEAL_COOLDOWN)
        {
            healTimer = 0;
            Player.Heal(GetHealAmount());
        }
    }
    public override void PostUpdate()
    {
        if (enabled) 
        {
            healTimer++;
            oldHealAmount = GetHealAmount();
        }
    }
}
