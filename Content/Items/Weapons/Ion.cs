using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class Ion : ModItem
{
    private const int ELECTRO_CHANCE = 100;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ELECTRO_CHANCE);
    
    public override void SetDefaults()
    {
        Item.damage = 120;
        Item.crit = -2;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 32;
        Item.height = 32;
        Item.scale = 1.8f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.rare = ItemRarityID.Cyan;
        Item.value = Item.sellPrice(gold: 12);
        Item.shoot = ModContent.ProjectileType<IonProjectile>();
        Item.shootSpeed = 16f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentStardust, 8);
        recipe.AddIngredient(ItemID.FragmentNebula, 8);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        ElectricityBuff.Create(damageDone, target);
    }
}