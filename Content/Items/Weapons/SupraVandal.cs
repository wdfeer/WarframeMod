using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class SupraVandal : Supra
{
    public const int WEAK_CHANCE = 20;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 79;
        Item.crit = 12;
        Item.width = 17;
        Item.height = 47;
        Item.value *= 2;
    }
    protected override int BaseUseTime => 15;
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient<Supra>();
        recipe.AddIngredient(ItemID.FragmentNebula, 12);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity * 4, ProjectileID.LaserMachinegunLaser, damage, knockback, timeSinceLastShot > 20 ? 0 : 0.06f, 50);
        proj.GetGlobalProjectile<BuffGlobalProjectile>().AddBuff(new Common.BuffChance(BuffID.Weak, 300, WEAK_CHANCE));
        return false;
    }
}
