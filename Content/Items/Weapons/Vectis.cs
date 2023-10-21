using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;
public class Vectis : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 100;
        Item.crit = 21;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 63;
        Item.height = 17;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6;
        Item.value = Item.sellPrice(silver: 75);
        Item.rare = 3;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 16f;
        Item.shoot = 10;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/VectisPrimeSound2").ModifySoundStyle(1f, 0.1f, 2);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteoriteBar, 8);
        recipe.AddIngredient(ItemID.Wire, 8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, ProjectileID.SniperBullet, damage, knockback, spawnOffset: 16);
        proj.friendly = true;
        return false;
    }
}