using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class KuvaKraken : ModItem
{
    public const int ALT_FIRE_DOWNTIME = 180;
    public override void SetDefaults()
    {
        Item.damage = 25;
        Item.crit = 17;
        Item.knockBack = 1.75f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 32;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 2;
        Item.useAnimation = 23;
        Item.useLimitPerAnimation = 3;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 2);
        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 12f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient<Kraken>();
        recipe.AddIngredient<Kuva>(2);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(4f, -1f);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item11, position);
        Projectile p = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.025f, Item.width);
        p.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.15f;
        return false;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useTime = 3;
            Item.useLimitPerAnimation = 21;
            Item.useAnimation = ALT_FIRE_DOWNTIME;
        }
        else
        {
            Item.useTime = 2;
            Item.useAnimation = 23;
            Item.useLimitPerAnimation = 3;
        }
        return base.CanUseItem(player);
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
}