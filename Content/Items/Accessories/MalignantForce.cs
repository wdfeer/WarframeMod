using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;
public class MalignantForce : ModItem
{
    public const int TOXIN_CHANCE = 15;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BuffPlayer>().AddBuffChance(StackableBuff.Toxin, TOXIN_CHANCE);
    }
    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ModContent.ItemType<InfectedClip>())
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddTile(TileID.Anvils)
            .Register();
}