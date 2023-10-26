using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Veldt : ModItem
{
    public const int BLEED_CHANCE = 22;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.crit = 18;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 48;
        Item.height = 20;
        Item.scale = 1.25f;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 4f;
        Item.value = Item.sellPrice(silver: 75);
        Item.rare = 2;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 16f;
        Item.shoot = 10;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = SoundID.Item40;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(0, -2);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, spawnOffset: 16);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.1f;
        proj.GetGlobalProjectile<BuffGlobalProjectile>().AddBleed(BLEED_CHANCE);
        return false;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteoriteBar, 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}