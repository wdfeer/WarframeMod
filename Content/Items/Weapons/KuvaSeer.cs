using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;


namespace WarframeMod.Content.Items.Weapons;
public class KuvaSeer : ModItem
{
    public const int EXPLOSION_DAMAGE_PERCENT = 50;
    public const int ICHOR_CHANCE = 33;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(EXPLOSION_DAMAGE_PERCENT, ICHOR_CHANCE);
    public override void SetDefaults()
    {
        Item.damage = 69;
        Item.crit = 17;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 19;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 4;
        Item.value = Item.sellPrice(gold: 1);
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = false;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-0.5f, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile proj = this.ShootWith(player, source, position, velocity, ModContent.ProjectileType<KuvaSeerProjectile>(), damage, knockback, 0.01f, Item.width);
        proj.GetGlobalProjectile<BuffGlobalProjectile>().AddBuff(new Common.BuffChance(BuffID.Ichor, 300, ICHOR_CHANCE));
        return false;
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient<Seer>();
        recipe.AddIngredient<Kuva>(2);
        recipe.Register();
    }
}
