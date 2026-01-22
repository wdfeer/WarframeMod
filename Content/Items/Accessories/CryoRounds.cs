using WarframeMod.Common;
using WarframeMod.Common.Players;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class CryoRounds : ModItem
{
    public const int CHANCE_PERCENT = 15;
    public const int DEBUFF_DURATION_FRAMES = ColdDebuff.DEFAULT_DURATION;
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
        buffman.buffsOnHitNPC.Add(new BuffChance(ModContent.BuffType<ColdDebuff>(),
            DEBUFF_DURATION_FRAMES,
            CHANCE_PERCENT));
    }
    public override void AddRecipes()
        => CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar, 3)
                         .AddIngredient(ItemID.IceBlock, 50)
                         .AddTile(TileID.Anvils)
                         .Register();
}
