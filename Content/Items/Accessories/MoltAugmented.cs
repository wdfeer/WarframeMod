using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class MoltAugmented : ModItem
{
    public const float PERCENT_DAMAGE_PER_KILL = 0.12f;
    public const int MAX_STACKS = 200;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On kill: +{PERCENT_DAMAGE_PER_KILL:0.00}% Damage\nStacks up to {MAX_STACKS} times\n50% Reduced effectiveness when a boss is alive");
    }

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = -12;
        Item.expert = true;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 6);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AugmentedPlayer>().enabled = true;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        Player player = Main.LocalPlayer;
        AugmentedPlayer augmentedPlayer = player.GetModPlayer<AugmentedPlayer>();
        if (augmentedPlayer.enabled)
        {
            int expertIndex = tooltips.FindIndex(tip => tip.Text == "Expert");
            TooltipLine line = new(Mod, "ActiveBonus", $"Current bonus is {augmentedPlayer.CurrentBonusPercent:0.00}%");
            tooltips.Insert(expertIndex, line);
        }
    }
}
class AugmentedPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public float CurrentBonusPercent => PercentDamagePerStack * stacks;
    public float PercentDamagePerStack => MoltAugmented.PERCENT_DAMAGE_PER_KILL / (Main.npc.Any(npc => npc.active && npc.boss) ? 2 : 1);
    public int stacks = 0;
    public override void PostUpdateEquips()
    {
        if (!enabled)
            stacks = 0;
        else
        {
            Player.GetDamage(DamageClass.Generic) += CurrentBonusPercent / 100f;
        }
    }
    public override void UpdateDead()
    {
        stacks = 0;
    }
    public void OnKillNPCWhenEnabled()
    {
        if (stacks < MoltAugmented.MAX_STACKS)
            stacks++;
    }
}
class AugmentedGlobalNPC : GlobalNPC
{
    public override void HitEffect(NPC npc, int hitDirection, double damage)
    {
        if (npc.life > 0)
            return;
        Player killer = Main.LocalPlayer;
        if (!killer.GetModPlayer<AugmentedPlayer>().enabled)
            return;
        killer.GetModPlayer<AugmentedPlayer>().OnKillNPCWhenEnabled();
    }
}