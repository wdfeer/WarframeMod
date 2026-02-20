using Terraria.DataStructures;
using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;

public class TenetFluxRifle : ModItem
{
    public const int BLEED_CHANCE = 100;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);

    public override void SetDefaults()
    {
        Item.damage = 40;
        Item.crit = 16;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 12;
        Item.width = 45;
        Item.height = 15;
        Item.useTime = 5;
        Item.useAnimation = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0.2f;
        Item.value = Item.sellPrice(gold: 4);
        Item.rare = 9;
        Item.autoReuse = true;
        Item.shootSpeed = 8f;
        Item.shoot = ModContent.ProjectileType<Projectiles.FluxRifleProjectile>();
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/TenetFluxRifleSound")
            .ModifySoundStyle(volume: 0.75f, pitchVariance: 0.12f);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4, 0);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<FluxRifle>();
        recipe.AddIngredient<Fieldron>();
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register(); 
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type,
        ref int damage,
        ref float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type,
        int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback);
        proj.penetrate = 1;
        return false;
    }
}