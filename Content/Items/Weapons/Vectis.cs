using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using WarframeMod.Content.Items;

namespace WarframeMod.Content.Items.Weapons;
public class Vectis : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shots penetrate an enemy");
    }
    public override void SetDefaults()
    {
        Item.damage = 100;
        Item.crit = 21;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 63;
        Item.height = 17;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6;
        Item.value = 15000;
        Item.rare = 4;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 16f;
        Item.shoot = 10;
        Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/VectisPrimeSound2").ModifySoundStyle(1f, 0.1f, 2);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteoriteBar, 8);
        recipe.AddIngredient(ItemID.Wire, 8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, spawnOffset: Item.width - 3);
        (Mod as WarframeMod).SetProjectileExtraUpdatesNetSafe(proj, proj.extraUpdates + 1);
        proj.penetrate = 2;
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = -1;
        return false;
    }
}