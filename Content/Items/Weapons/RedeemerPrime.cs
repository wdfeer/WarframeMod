using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class RedeemerPrime : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Fires 6 golden pellets without consuming ammo\nLinear damage falloff starting at 25 tiles");
    }
    public override void SetDefaults()
    {
        Item.damage = 44;
        Item.crit = 20;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.width = 48;
        Item.height = 24;
        Item.scale = 1f;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 7;
        Item.value = 15000;
        Item.rare = 5;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.GoldenBullet;
        Item.shootSpeed = 16f;
        Item.UseSound = new SoundStyle("WarframeMod/Content/Sounds/RedeemerPrimeSound").ModifySoundStyle(0.45f, 0.1f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Redeemer>());
        recipe.AddIngredient(ItemID.HallowedBar, 8);
        recipe.AddIngredient(ItemID.SoulofFright, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int i = 0; i < 6; i++)
        {
            var projectile = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.13f, Item.width);
            projectile.DamageType = DamageClass.Melee;
            projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.1f;
            projectile.GetGlobalProjectile<FalloffGlobalProjectile>().SetFalloff(projectile.position, 25 * 16, 45 * 16, 0.5f);
        }
        return false;
    }
}