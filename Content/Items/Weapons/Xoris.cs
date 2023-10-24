using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Xoris : BaseGlaive
{
    public const int BIG_BOOM_DAMAGE_MULT = 3;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 332;
        Item.crit = 20;
        Item.value = Item.sellPrice(gold: 15);
        Item.rare = ItemRarityID.Red;
        Item.shoot = ModContent.ProjectileType<XorisProjectile>();
        Item.shootSpeed = 24f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Falcor>());
        recipe.AddIngredient(ItemID.FragmentSolar, 8);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    int explosionCount = 1;
    public override void OnShoot() { }
    public override void PreExplode()
    {
        Proj.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(0.18f);

        var xorisProj = Proj.ModProjectile as XorisProjectile;
        if (explosionCount >= 3)
        {
            xorisProj.SetBigBoom();
            explosionCount = 0;
        }
        explosionCount++;
    }
}