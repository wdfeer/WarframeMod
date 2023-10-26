using System.Security.Policy;
using WarframeMod.Common.GlobalItems;

namespace WarframeMod.Content.Items.Weapons;

internal class Hate : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 29;
        Item.crit = 26;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 48;
        Item.scale = 1.2f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 2);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 1.25f;
        Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = 0.2f;
    }
}