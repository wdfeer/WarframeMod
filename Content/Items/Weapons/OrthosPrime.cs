using Terraria.Localization;
using WarframeMod.Common.GlobalItems;
using Terraria.ID;


namespace WarframeMod.Content.Items.Weapons;

internal class OrthosPrime : Orthos
{
    public const int CRIT_DAMAGE_BONUS_PERCENT = 10;
    public const int BLEED_CHANCE = 36;
    protected override float BaseBleedChance => BLEED_CHANCE / 100f;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE, $"+{CRIT_DAMAGE_BONUS_PERCENT}%");
    public override void SetDefaults()
    {
        Item.damage = 100;
        Item.crit = 20;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 47;
        Item.height = 48;
        Item.scale = 2.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.buyPrice(gold: 8);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 1f + CRIT_DAMAGE_BONUS_PERCENT / 100f;
        Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BaseBleedChance;
    }
    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ItemID.HallowedBar, 16)
                         .AddTile(TileID.MythrilAnvil)
                         .Register();
}