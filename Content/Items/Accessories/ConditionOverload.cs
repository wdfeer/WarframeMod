using WarframeMod.Common;
using WarframeMod.Common.Players;
using Terraria.Localization;
using WarframeMod.Common.GlobalNPCs;


namespace WarframeMod.Content.Items.Accessories;

public class ConditionOverload : ModItem
{
    public const int DAMAGE_INCREASE = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DAMAGE_INCREASE);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(gold: 10);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ConditionOverloadPlayer>().enabled = true;
    }
}

class ConditionOverloadPlayer : ModPlayer
{
    public bool enabled;

    public override void ResetEffects()
        => enabled = false;

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        if (enabled)
            modifiers.FinalDamage *= 1f + (target.GetStatusCount() * ConditionOverload.DAMAGE_INCREASE / 100f);
    }
}