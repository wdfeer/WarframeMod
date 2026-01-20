namespace WarframeMod.Content.Items.Accessories;

public class AmarHatred : AmarAccessory
{
    private const int DEFENSE_INCREASE = 7;
    private const int MELEE_DAMAGE_INCREASE_PERCENT = 10;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(silver: 33);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        player.statDefense += DEFENSE_INCREASE;
        player.GetDamage(DamageClass.Melee) += MELEE_DAMAGE_INCREASE_PERCENT / 100f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<SteelFiber>());
        recipe.AddIngredient(ItemID.WarriorEmblem);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}
