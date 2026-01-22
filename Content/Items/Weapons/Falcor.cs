using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;
public class Falcor : BaseGlaive
{
    public const int ELECTRO_CHANCE = 100;
    public const int BLEED_CHANCE = 36;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 166;
        Item.crit = 10;
        Item.value = Item.sellPrice(gold: 7);
        Item.rare = ItemRarityID.Yellow;
        Item.shoot = ModContent.ProjectileType<FalcorProjectile>();
        Item.shootSpeed = 18f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LightDisc);
        recipe.AddIngredient(ModContent.ItemType<Fieldron>(), 1);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void OnShoot()
    {
        var buffProjectile = Proj.GetGlobalProjectile<BuffGlobalProjectile>();
        buffProjectile.AddBleed(BLEED_CHANCE / 100f);
    }

    public override void PreExplode()
    {
        var buffProjectile = Proj.GetGlobalProjectile<BuffGlobalProjectile>();
        buffProjectile.AddElectro(ELECTRO_CHANCE / 100f);
    }

}