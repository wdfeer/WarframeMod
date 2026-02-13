using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Common.Players;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Arcanes;

public class VirtuosTrojan : Arcane
{
    public const int COLD_CHANCE = 10;
    public const int TOXIN_CHANCE = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(COLD_CHANCE, TOXIN_CHANCE);
    
    public override void UpdateArcane(Player player)
    {
        BuffPlayer buffman = player.GetModPlayer<BuffPlayer>();
        buffman.buffsOnHitNPC.Add(new BuffChance(ModContent.BuffType<ColdDebuff>(), 300, COLD_CHANCE));
        buffman.AddBuffChance(StackableBuff.Toxin, TOXIN_CHANCE);
    }
}