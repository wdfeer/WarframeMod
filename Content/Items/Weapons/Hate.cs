using Terraria.Localization;
using WarframeMod.Common.GlobalItems;

namespace WarframeMod.Content.Items.Weapons;

internal class Hate : ModItem
{
    public const int CRIT_DAMAGE_BONUS = 25;
    public const int BLEED_CHANCE = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRIT_DAMAGE_BONUS}%", BLEED_CHANCE);
    public override void SetDefaults()
    {
        Item.damage = 35;
        Item.crit = 26;
        Item.knockBack = 4f;
        Item.pick = 109;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 48;
        Item.scale = 1.2f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 5);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 1f + CRIT_DAMAGE_BONUS / 100f;
        Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BLEED_CHANCE / 100f;
    }
}