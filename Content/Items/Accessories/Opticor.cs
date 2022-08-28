using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Accessories;
public class Opticor : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Charges to shoot a devastating beam\n+25% Critical Damage");
    }
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/OpticorSound");
        Item.channel = true;
        Item.damage = 690;
        Item.crit = 16;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 77;
        Item.width = 48;
        Item.height = 16;
        Item.useTime = 124;
        Item.useAnimation = 124;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 20;
        Item.value = Item.buyPrice(gold: 10);
        Item.rare = 4;
        Item.autoReuse = false;
        Item.shoot = ModContent.ProjectileType<OpticorProjectile>();
        Item.shootSpeed = 16f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofSight, 7);
        recipe.AddIngredient(ItemID.TitaniumBar, 11);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SoulofSight, 7);
        recipe.AddIngredient(ItemID.AdamantiteBar, 11);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.25f;
        return false;
    }
}