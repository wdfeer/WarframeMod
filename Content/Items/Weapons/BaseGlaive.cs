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
public abstract class BaseGlaive : ModItem
{
    public override void SetDefaults()
    {
        Item.knockBack = 4;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = 32;
        Item.height = 32;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shootSpeed = 16f;
        Item.UseSound = SoundID.Item1;
    }
    static Dictionary<int, Projectile> glaiveProjectiles = new();
    protected Projectile Proj
    {
        get => glaiveProjectiles[Type];
        set => glaiveProjectiles[Type] = value;
    }
    public abstract void OnShoot();
    public abstract void PreExplode();
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (glaiveProjectiles.ContainsKey(Type) && Proj != null && Proj.active && Proj.type == type)
        {
            PreExplode();
            (Proj.ModProjectile as ExplosiveProjectile).Explode();
        }
        else
        {
            Proj = WeaponCommon.ShootWith(this, player, source, position, velocity, type, damage, knockback);
            OnShoot();
        }

        return false;
    }
}