using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Common;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Falcor : ModItem
{
    public const int ELECTRO_CHANCE = 100;
    public const int BLEED_CHANCE = 36;
    public override void SetDefaults()
    {
        Item.damage = 166;
        Item.crit = 10;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = 32;
        Item.height = 32;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 4;
        Item.value = Item.sellPrice(gold: 7);
        Item.rare = 8;
        Item.shoot = ModContent.ProjectileType<FalcorProjectile>();
        Item.shootSpeed = 18f;
        Item.UseSound = SoundID.Item1;
    }
    static Projectile proj;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        if (proj != null && proj.active && proj.ModProjectile is FalcorProjectile)
        {
            var buffProjectile = proj.GetGlobalProjectile<BuffGlobalProjectile>();
            buffProjectile.AddElectro(ELECTRO_CHANCE / 100f);

            (proj.ModProjectile as ExplosiveProjectile).Explode();
        }
        else
        {
            proj = WeaponCommon.ShootWith(this, player, source, position, velocity, type, damage, knockback);
            var buffProjectile = proj.GetGlobalProjectile<BuffGlobalProjectile>();
            buffProjectile.AddBleed(BLEED_CHANCE / 100f);
        }

        return false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LightDisc);
        recipe.AddIngredient(ModContent.ItemType<Fieldron>(), 1);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}