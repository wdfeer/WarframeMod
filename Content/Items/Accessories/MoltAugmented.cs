using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class MoltAugmented : ModItem
{
    public const float PERCENT_DAMAGE_PER_KILL = 0.12f;
    public const int MAX_STACKS = 150;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"On kill: +{PERCENT_DAMAGE_PER_KILL:0.0}% Damage\nStacks up to {MAX_STACKS} times");
    }

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = -12;
        Item.expert = true;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 5);
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
            TooltipLine line = new(Mod, "ActiveBonus", $"Current bonus is {augmentedPlayer.currentBonus:0.00}%");
            tooltips.Insert(expertIndex, line);
        }
    }
}
class AugmentedPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public float currentBonus = 0f;
    public int Stacks => (int)(currentBonus / MoltAugmented.PERCENT_DAMAGE_PER_KILL);
    public override void PostUpdateEquips()
    {
        if (!enabled)
            currentBonus = 0f;
        else
        {
            Player.GetDamage(DamageClass.Generic) += currentBonus / 100f;
        }
    }
    public void OnKillNPCWhenEnabled()
    {
        if (Stacks < MoltAugmented.MAX_STACKS)
            currentBonus += MoltAugmented.PERCENT_DAMAGE_PER_KILL;
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