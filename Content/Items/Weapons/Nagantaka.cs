using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class Nagantaka : ModItem
{
    public const int ALT_FIRE_DOWNTIME = 150;
    public const int CRITICAL_DAMAGE_PERCENT = 15;
    public const int BLEED_CHANCE_PERCENT = 100;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs($"+{CRITICAL_DAMAGE_PERCENT}%", BLEED_CHANCE_PERCENT);
    public override void SetDefaults()
    {
        Item.damage = 40;
        Item.crit = 11;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 50;
        Item.height = 22;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.rare = ItemRarityID.LightPurple;
        Item.value = Item.sellPrice(gold: 6);
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Arrow;
    }
    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
        recipe.AddIngredient(ItemID.DarkShard);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(8f, -2f);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item5, position);
        
        float spread = player.altFunctionUse == 2 ? 0.08f : 0.025f;
        Projectile p = this.ShootWith(player, source, position, velocity, type, damage, knockback, spread, Item.width);
        
        p.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 1 + CRITICAL_DAMAGE_PERCENT / 100f;
        p.GetGlobalProjectile<BuffGlobalProjectile>().AddBleed(BLEED_CHANCE_PERCENT);
        
        if (player.altFunctionUse != 2)
        {
            p.extraUpdates += 1;
        }
        
        return false;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useTime = 5;
            Item.useLimitPerAnimation = 9;
            Item.useAnimation = ALT_FIRE_DOWNTIME;
        }
        else
        {
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useLimitPerAnimation = 1;
        }
        return base.CanUseItem(player);
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
}