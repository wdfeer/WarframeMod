using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Kunai : ModItem
{
    public const int BLEED_CHANCE = 15;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);

    public override void SetDefaults()
    {
        Item.damage = 21;
        Item.crit = 14;
        Item.knockBack = 0.8f;
        Item.DamageType = Calamity.rogue != null ? Calamity.rogue : DamageClass.Ranged;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.autoReuse = true;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 60);
        Item.shoot = ModContent.ProjectileType<KunaiProjectile>();
        Item.shootSpeed = 24f;
    }
}