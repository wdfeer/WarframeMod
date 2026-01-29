using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class DexAfuris : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 24;
        Item.height = 20;
        Item.useTime = 3;
        Item.useAnimation = 3;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0.1f;
        Item.value = 5000;
        Item.rare = 4;
        Item.UseSound = SoundID.Item11.WithVolumeScale(0.6f);
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override void AddRecipes()
        => CreateRecipe().AddIngredient<Afuris>().AddIngredient(ItemID.LightShard)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.07f, Item.width);
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = 2;
        return false;
    }
}