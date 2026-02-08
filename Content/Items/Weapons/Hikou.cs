using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Hikou : ModItem
{
    public const int DEFENSE_PENETRATION = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DEFENSE_PENETRATION);

    public override void SetDefaults()
    {
        Item.damage = 16;
        Item.crit = 0;
        Item.knockBack = 0.2f;
        Item.DamageType = Calamity.rogue != null ? Calamity.rogue : DamageClass.Ranged;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.autoReuse = true;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 2);
        Item.shoot = ModContent.ProjectileType<HikouProjectile>();
        Item.shootSpeed = 20f;
        Item.ArmorPenetration = DEFENSE_PENETRATION;
    }
}