using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Tetra : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("-25% Critical Damage");
    }
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/TenetTetraPrimarySound").ModifySoundStyle(pitchVariance: 0.1f);
        Item.damage = 22;
        Item.crit = 0;
        Item.mana = 3;
        Item.DamageType = DamageClass.Magic;
        Item.width = 40;
        Item.height = 14;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 3;
        Item.value = Item.sellPrice(gold: 1);
        Item.rare = 2;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(2, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity * 2f, ProjectileID.LaserMachinegunLaser, damage, knockback, 0.04f, Item.width);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 0.75f;
        return false;
    }
}