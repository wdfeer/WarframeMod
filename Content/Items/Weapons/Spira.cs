using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Spira : ModItem
{
    public const int DEFENSE_PENETRATION = 40;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DEFENSE_PENETRATION);

    public override void SetDefaults()
    {
        Item.damage = 120;
        Item.crit = 26;
        Item.knockBack = 1f;
        Item.DamageType = Calamity.rogue != null ? Calamity.rogue : DamageClass.Ranged;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = Item.useAnimation = 24;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = Item.sellPrice(gold: 2);
        Item.shoot = ModContent.ProjectileType<SpiraProjectile>();
        Item.shootSpeed = 28f;
        Item.ArmorPenetration = DEFENSE_PENETRATION;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<Kunai>()
            .AddIngredient(ItemID.ChlorophyteBar, 12)
            .AddIngredient(ItemID.Silk, 20)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}