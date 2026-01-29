using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class Afuris : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 3;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 27;
        Item.height = 22;
        Item.useTime = 3;
        Item.useAnimation = 3;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0;
        Item.value = 5000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item11.WithVolumeScale(0.7f);
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override void AddRecipes()
        => CreateRecipe().AddIngredient<Furis>(2).AddIngredient(ItemID.BeeWax, 4).AddTile(TileID.TinkerersWorkbench)
            .Register();

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.06f, Item.width);
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = 2;
        return false;
    }
}