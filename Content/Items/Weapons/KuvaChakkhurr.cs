using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class KuvaChakkhurr : ModItem
{
    public const int EXPLOSION_DAMAGE_PERCENT = 33;
    public const int CRITICAL_DAMAGE_BONUS_PERCENT = 15;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(EXPLOSION_DAMAGE_PERCENT, $"+{CRITICAL_DAMAGE_BONUS_PERCENT}%");
    public override void SetDefaults()
    {
        Item.damage = 116;
        Item.crit = 46;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 10;
        Item.scale = 1.7f;
        Item.useTime = 51;
        Item.useAnimation = 51;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 10;
        Item.value = Item.buyPrice(gold: 7);
        Item.rare = ItemRarityID.Pink;
        Item.autoReuse = false;
        Item.shootSpeed = 20f;
        Item.shoot = 10;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/KuvaChakkhurrSound").ModifySoundStyle(0.5f, 0.1f);
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(0, 4);
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TitaniumBar, 8);
        recipe.AddIngredient<Kuva>(3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AdamantiteBar, 8);
        recipe.AddIngredient<Kuva>(3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = WeaponCommon.ShootWith(this, player, source, position, velocity, ModContent.ProjectileType<KuvaChakkhurrProjectile>(), damage, knockback, spawnOffset: Item.width * Item.scale + 2);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier += CRITICAL_DAMAGE_BONUS_PERCENT / 100f;
        return false;
    }
}