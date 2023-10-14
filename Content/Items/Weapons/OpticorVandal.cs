using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class OpticorVandal : ModItem
{
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/OpticorVandalSound").ModifySoundStyle();
        Item.channel = true;
        Item.damage = 850;
        Item.crit = 20;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 36;
        Item.width = 48;
        Item.height = 16;
        Item.useTime = 76;
        Item.useAnimation = 76;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 20;
        Item.value = Item.buyPrice(gold: 16);
        Item.rare = 10;
        Item.autoReuse = false;
        Item.shoot = ModContent.ProjectileType<OpticorVandalProjectile>();
        Item.shootSpeed = 16f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Opticor>());
        recipe.AddIngredient(ItemID.FragmentNebula, 16);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1.3f;
        return false;
    }
}