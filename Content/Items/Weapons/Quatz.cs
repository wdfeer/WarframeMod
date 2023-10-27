using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;
public class Quatz : ModItem
{
    public const int ELECTRO_CHANCE_AUTO = 27;
    public const int AMMO_SAVE_CHANCE_AUTO = 70;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ELECTRO_CHANCE_AUTO, AMMO_SAVE_CHANCE_AUTO);
    public override void SetDefaults()
    {
        Item.damage = 3;
        Item.crit = 9;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 19;
        Item.scale = 0.8f;
        Item.useTime = 4;
        Item.useAnimation = 4;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = 1500;
        Item.rare = 3;
        Item.UseSound = SoundID.Item11.WithVolumeScale(0.6f);
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo Item that this weapon uses. Note that this is not an Item Id, but just a magic value.
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ShadowScale, 12);
        recipe.AddIngredient(ItemID.TungstenBar, 8);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ShadowScale, 12);
        recipe.AddIngredient(ItemID.SilverBar, 8);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.crit = 23;
            Item.shootSpeed = 20f;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item11;
        }
        else
        {
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.crit = 9;
            Item.shootSpeed = 16f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item11.WithVolumeScale(0.6f);
        }
        return base.CanUseItem(player);
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (player.altFunctionUse != 2 && Main.rand.Next(100) < AMMO_SAVE_CHANCE_AUTO)
            return false;
        return base.CanConsumeAmmo(ammo, player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int i = 0; i < (player.altFunctionUse == 2 ? 4 : 1); i++)
        {
            float spreadMult = player.altFunctionUse == 2 ? 0.012f : 0.024f;
            var projectile = WeaponCommon.ShootWith(this, player, source, position, velocity, type, damage, knockback, spreadMult, Item.width);
            if (player.altFunctionUse == 2)
            {
                projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.25f;
                projectile.damage = (int)(projectile.damage * 1.5f);
                projectile.knockBack += 2f;
            }
            else
                projectile.GetGlobalProjectile<BuffGlobalProjectile>().AddElectro(ELECTRO_CHANCE_AUTO);
        }

        return false;
    }
}