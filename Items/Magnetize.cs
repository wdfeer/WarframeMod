using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Linq;
using WarframeMod.Projectiles;

namespace WarframeMod.Items
{
    public class Magnetize : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Creates a magnetizing sphere around the enemy\nAll friendly projectiles are accelerated towards the center of the sphere");
        }
        public override void SetDefaults()
        {
            Item.mana = 128;
            Item.scale = 0f;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.value = Item.buyPrice(gold: 20);
            Item.rare = 5;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<MagnetizeProjectile>();
            Item.shootSpeed = 1f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            NPC target = FindTarget(Main.MouseWorld);
            if (target == null)
                return false;
            position = target.Center;
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            MagnetizeProjectile modProj = projectile.ModProjectile as MagnetizeProjectile;
            modProj.target = target;
            return false;
        }
        private NPC FindTarget(Vector2 mousePosition)
        {
            NPC potentialTarget = Main.npc.MinBy(x => x.Center.Distance(mousePosition));
            if (!Main.projectile.Any(p => p.active
                                          && p.ModProjectile is MagnetizeProjectile
                                          && (p.ModProjectile as MagnetizeProjectile).target == potentialTarget) 
                                          && potentialTarget.CanBeChasedBy() 
                                          && potentialTarget.Center.Distance(mousePosition) < 80f)
                return potentialTarget;
            return null;
        }
    }
}