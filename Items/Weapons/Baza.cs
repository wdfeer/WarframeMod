using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WarframeMod.Global;

namespace WarframeMod.Items.Weapons
{
    public class Baza : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("75% Chance not to consume ammo\n+50% Critical Damage");
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.crit = 22;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 45;
            Item.height = 18;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 15000;
            Item.rare = 3;
            Item.UseSound = WeaponCommon.ModifySoundStyle(SoundID.Item11, 0.1f);
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo Item that this weapon uses. Note that this is not an Item Id, but just a magic value.
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4,0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 75) return false;
            return base.CanConsumeAmmo(ammo, player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            WarframeGlobalProjectile globalProjectile = projectile.GetGlobalProjectile<WarframeGlobalProjectile>();
            globalProjectile.CritMultiplier = 1.5f;
            return false;
        }
    }
}