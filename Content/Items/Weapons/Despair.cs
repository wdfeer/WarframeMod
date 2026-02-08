using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Despair : ModItem
{
    public const int CURSED_FLAMES_CHANCE = 30;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CURSED_FLAMES_CHANCE);

    public override void SetDefaults()
    {
        Item.damage = 66;
        Item.crit = 12;
        Item.knockBack = 1.2f;
        Item.DamageType = Calamity.rogue != null ? Calamity.rogue : DamageClass.Ranged;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = Item.useAnimation = 20;
        Item.autoReuse = true;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 4);
        Item.shoot = ModContent.ProjectileType<DespairProjectile>();
        Item.shootSpeed = 24f;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CobaltBar, 10)
            .AddIngredient(ItemID.DarkShard)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddTile(TileID.Anvils)
            .Register();
        
        CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 16)
            .AddIngredient(ItemID.DarkShard)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
}