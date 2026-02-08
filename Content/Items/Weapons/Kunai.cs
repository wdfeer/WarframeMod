using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using WarframeMod.Common;
using WarframeMod.Content.Items.Consumables;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Kunai : ModItem
{
    public const int BLEED_CHANCE = 15;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);

    public bool stockpiledBlight;

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
        Item.useTime = Item.useAnimation = 20;
        Item.autoReuse = true;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 60);
        Item.shoot = ModContent.ProjectileType<KunaiProjectile>();
        Item.shootSpeed = 24f;
        UpdateUpgradeStats();
    }

    public void UpdateUpgradeStats()
    {
        if (stockpiledBlight)
        {
            Item.damage += StockpiledBlight.BASE_DAMAGE_FLAT;
            Item.useTime = Item.useAnimation = (int)(Item.useTime / (1f + StockpiledBlight.FIRE_RATE_BONUS / 100f));
            Item.rare = 5;
            Item.value = Item.sellPrice(gold: 6);
        }
    }

    public override void LoadData(TagCompound tag)
    {
        stockpiledBlight = tag.ContainsKey("stockpiledBlight") && tag.GetBool("stockpiledBlight");
    }

    public override void SaveData(TagCompound tag)
    {
        tag["stockpiledBlight"] = stockpiledBlight;
    }
}