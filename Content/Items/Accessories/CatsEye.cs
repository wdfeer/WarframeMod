using WarframeMod.Common;
using WarframeMod.Common.Players;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class CatsEye : ModItem
{
    public const int COOLDOWN = 25 * 60;
    public const int CRIT = 50;
    public const int DURATION = 10 * 60;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Cat's Eye");
        Tooltip.SetDefault($"Every {COOLDOWN / 60} seconds provides +{CRIT}% summon Critical Chance for {DURATION / 60} seconds");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 2);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SummonerEmblem);
        recipe.AddIngredient(ItemID.SoulofNight, 8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    int timer = 0;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!player.HasBuff<CatsEyeBuff>())
        {
            timer++;
            if (timer >= COOLDOWN)
            {
                player.AddBuff(ModContent.BuffType<CatsEyeBuff>(), DURATION);
                timer = 0;
            }
        }
    }
}
