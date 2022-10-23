using WarframeMod.Common;
using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Common.Players;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class CryoRounds : ModItem
{
    public const int CHANCE_PERCENT = 15;
    public const int DEBUFF_DURATION = 360;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($@"+{CHANCE_PERCENT}% chance to slow enemies down for {DEBUFF_DURATION / 60} seconds
Slowed enemies under the effects of poison or venom take {ViralGlobalNPC.EXTRA_DAMAGE_PERCENT}% more damage");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 25);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        BuffPlayer buffman = player.GetModPlayer<BuffPlayer>();
        buffman.buffsOnHitNPC.Add(new BuffChance(ModContent.BuffType<ColdDebuff>(), DEBUFF_DURATION, CHANCE_PERCENT));
    }
    public override void AddRecipes()
        => CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar, 3)
                         .AddIngredient(ItemID.IceBlock, 50)
                         .AddTile(TileID.Anvils)
                         .Register();
}
