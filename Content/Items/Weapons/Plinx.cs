using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Weapons;
public class Plinx : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+50% Critical Damage\nTripled effect from relative critical chance bonuses");
    }
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.crit = 28;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 20;
        Item.width = 40;
        Item.height = 15;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2.25f;
        Item.value = Item.sellPrice(gold: 3);
        Item.rare = 4;
        Item.shoot = ProjectileID.LaserMachinegunLaser;
        Item.shootSpeed = 20f;
        Item.UseSound = SoundID.Item11;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.IllegalGunParts);
        recipe.AddIngredient(ItemID.SoulofLight, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var projectile = this.ShootWith(player, source, position, velocity, type, damage, knockback, spawnOffset: 25);
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.5f;

        return false;
    }
    public override void ModifyWeaponCrit(Player player, ref float crit)
    {
        crit += (Item.crit + 4) * player.GetModPlayer<CritPlayer>().relativeCritChance * 2;
    }
}