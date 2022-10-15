using Terraria.GameContent.ItemDropRules;
using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class BleedingDragonKey : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("-75% max life\nBosses drop corrupted mods");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 3);
    }
    public override bool ReforgePrice(ref int reforgePrice, ref bool canApplyDiscount)
    {
        reforgePrice *= 10;
        return true;
    }
    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ItemID.ShadowKey)
                         .AddIngredient(ItemID.HellstoneBar, 10)
                         .AddTile(TileID.Anvils)
                         .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BleedingDragonKeyPlayer>().enabled = true;
    }
}
class BleedingDragonKeyPlayer : ModPlayer
{
    public bool enabled = false;
    public override void ResetEffects()
    {
        enabled = false;
    }
    public override void PostUpdateMiscEffects()
    {
        if (enabled)
            Player.statLifeMax2 /= 4;
    }
}
class DragonKeyCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) 
        => info.player.GetModPlayer<BleedingDragonKeyPlayer>().enabled;
    public bool CanShowItemDropInUI()
        => false;
    public string GetConditionDescription()
        => "You need to have a dragon key equipped";
}