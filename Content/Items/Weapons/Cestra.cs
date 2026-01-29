using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Cestra : ModItem
{
    public const int CRIT_DAMAGE_REDUCTION = 20;
    public const int DEFENSE_PENETRATION = 10;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs($"-{CRIT_DAMAGE_REDUCTION}%", DEFENSE_PENETRATION);

    public override void SetDefaults()
    {
        Item.UseSound =
            new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/SupraVandalSound").ModifySoundStyle(
                pitchVariance: 0.1f);
        Item.damage = 10;
        Item.crit = 2;
        Item.mana = 9;
        Item.DamageType = DamageClass.Magic;
        Item.width = 30;
        Item.height = 14;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 3.8f;
        Item.value = Item.sellPrice(gold: 2);
        Item.rare = 3;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.LaserMachinegunLaser;
        Item.shootSpeed = 16f;
        Item.maxStack = 2;
    }

    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ItemID.MeteoriteBar, 12).AddTile(TileID.Anvils).Register();

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-1, 0);
    }

    bool dualWield => Item.stack == 2;
    private float maxFireRateBonus => dualWield ? 2f : 1f;

    private float spoolUp;

    public override void UpdateInventory(Player player)
    {
        spoolUp = MathF.Max(spoolUp - 0.5f / 60, 0);
    }

    public override float UseSpeedMultiplier(Player player)
    {
        return 1f + spoolUp * maxFireRateBonus;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback,
            dualWield ? 0.07f : 0.03f, Item.width);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier -= CRIT_DAMAGE_REDUCTION / 100f;
        proj.ArmorPenetration = DEFENSE_PENETRATION;

        spoolUp = MathF.Min(spoolUp + 0.18f, 1f);

        return false;
    }
}