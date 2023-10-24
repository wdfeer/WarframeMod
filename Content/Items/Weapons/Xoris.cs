using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Xoris : ModItem
{
    public const int BIG_BOOM_DAMAGE_MULT = 3;
    public override void SetDefaults()
    {
        Item.damage = 332;
        Item.crit = 20;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = 32;
        Item.height = 32;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 4;
        Item.value = Item.sellPrice(gold: 15);
        Item.rare = ItemRarityID.Red;
        Item.shoot = ModContent.ProjectileType<XorisProjectile>();
        Item.shootSpeed = 24f;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Falcor>());
        recipe.AddIngredient(ItemID.FragmentSolar, 8);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    static Projectile proj;
    int explosionCount = 0;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (proj != null && proj.active && proj.ModProjectile is XorisProjectile)
        {
            var xorisProj = proj.ModProjectile as XorisProjectile;
            if (explosionCount >= 3)
            {
                xorisProj.SetBigBoom();
                explosionCount = 0;
            }
            xorisProj.Explode();
            explosionCount++;
        }
        else
        {
            proj = WeaponCommon.ShootWith(this, player, source, position, velocity, type, damage, knockback);
        }

        return false;
    }
}