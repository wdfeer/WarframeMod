using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Arcanes;

public class MoltAugmented : Arcane
{
    public override void SetStaticDefaults()
    {
        OnKillGlobalNPC.RegisterOnKillEvent(hit => hit.player.GetModPlayer<AugmentedPlayer>().enabled,
            hit =>
            {
                var modPlayer = hit.player.GetModPlayer<AugmentedPlayer>();
                if (modPlayer.stacks < MAX_STACKS)
                    modPlayer.stacks++;
            });
    }

    public const float PERCENT_DAMAGE_PER_KILL = 0.12f;
    public const int MAX_STACKS = 200;

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        Player player = Main.LocalPlayer;
        AugmentedPlayer augmentedPlayer = player.GetModPlayer<AugmentedPlayer>();
        if (augmentedPlayer.enabled)
        {
            int expertIndex = tooltips.FindIndex(tip => tip.Text == "Expert");
            if (expertIndex == -1)
                return;
            TooltipLine line = new(Mod, "ActiveBonus", $"Current bonus: +{augmentedPlayer.CurrentBonusPercent:0.00}%");
            tooltips.Insert(expertIndex, line);
        }
    }

    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<AugmentedPlayer>().enabled = true;
    }
}

class AugmentedPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects() => enabled = false;
    public float CurrentBonusPercent => PercentDamagePerStack * stacks;

    public float PercentDamagePerStack =>
        MoltAugmented.PERCENT_DAMAGE_PER_KILL / (Main.npc.Any(npc => npc.active && npc.boss) ? 2 : 1);

    public int stacks;

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
}