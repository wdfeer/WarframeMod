using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Accessories;

public class HunterCommand : HunterAccessory
{
    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(bleedChanceFormatArg);

    public const int DAMAGE_ON_BLEEDING_PERCENT = 15;

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 5;
        Item.value = Item.sellPrice(gold: 4);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        player.GetModPlayer<HunterCommandPlayer>().enabled = true;
    }
}

class HunterCommandPlayer : ModPlayer
{
    public bool enabled = false;

    public override void ResetEffects()
        => enabled = false;

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (enabled && (proj.DamageType == DamageClass.Summon || proj.DamageType == DamageClass.SummonMeleeSpeed) &&
            target.GetGlobalNPC<DotDebuffNpc>().timers.ContainsKey(StackableBuff.Bleeding))
            modifiers.SourceDamage *= 1f + (HunterCommand.DAMAGE_ON_BLEEDING_PERCENT / 100f);
    }
}