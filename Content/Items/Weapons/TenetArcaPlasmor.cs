using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class TenetArcaPlasmor : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots a piercing projectile\nMight confuse enemies");
    }
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/TenetArcaPlasmorSound").ModifySoundStyle(pitchVariance: 0.13f);
        Item.damage = 697;
        Item.crit = 18;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 13;
        Item.width = 48;
        Item.height = 16;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 8;
        Item.value = 150000;
        Item.rare = ItemRarityID.Cyan;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<ArcaPlasmorProjectile>();
        Item.shootSpeed = 30f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentNebula, 15);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var projectile = this.ShootWith(player, source, position, velocity, type, damage, knockback, spawnOffset: Item.width);
        projectile.extraUpdates = 1;
        projectile.timeLeft += 12;
        var modProj = projectile.ModProjectile as ArcaPlasmorProjectile;
        modProj.tenet = true;

        return false;
    }
}