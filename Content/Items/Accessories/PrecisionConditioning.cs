using WarframeMod.Common.Players;
using Terraria.Localization;


namespace WarframeMod.Content.Items.Accessories;

public class PrecisionConditioning : ModItem
{
    public const int SUMMON_DAMAGE_INCREASE_PERCENT = 15;
    public const int SUMMON_BLEED_CHANCE_PERCENT = 15;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs($"+{SUMMON_DAMAGE_INCREASE_PERCENT}%", $"+{SUMMON_BLEED_CHANCE_PERCENT}%");

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = Item.sellPrice(gold: 19);
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SummonerEmblem);
        recipe.AddIngredient(ItemID.ChainGuillotines);
        recipe.AddIngredient(ItemID.Cutlass);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SummonerEmblem);
        recipe.AddIngredient(ItemID.FetidBaghnakhs);
        recipe.AddIngredient(ItemID.Cutlass);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Summon) += SUMMON_DAMAGE_INCREASE_PERCENT / 100f;

        BuffPlayer buffman = player.GetModPlayer<BuffPlayer>();
        buffman.AddBleedChance(DamageClass.Summon, SUMMON_BLEED_CHANCE_PERCENT);
    }
}