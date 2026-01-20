using System.Security.Policy;
using Terraria.Localization;
using WarframeMod.Common.GlobalItems;
using Terraria.ID;


namespace WarframeMod.Content.Items.Weapons;

internal class Cronus : ModItem
{
    public const int BLEED_CHANCE = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);
    public override void SetDefaults()
    {
        Item.damage = 29;
        Item.crit = 2;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 37;
        Item.height = 40;
        Item.scale = 1.3f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 50);
        Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BLEED_CHANCE / 100f;
    }
}