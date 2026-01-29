using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class BallisticaPrime : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 60;
        Item.crit = 16;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 35;
        Item.height = 28;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 3;
        Item.value = Item.sellPrice(gold: 13);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = false;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 16;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override void AddRecipes()
        => CreateRecipe()
            .AddIngredient(ItemID.Phantasm)
            .AddIngredient(ItemID.HallowedBar, 12)
            .AddIngredient(ItemID.Silk, 10)
            .AddTile(TileID.LunarCraftingStation)
            .Register();

    double lastShotTime;
    private float Charge => (float)Math.Clamp((Main.time - lastShotTime) / Item.useTime, 1, 3);

    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        => damage *= Charge;

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        var timeSinceLastShot = Main.time - lastShotTime;
        lastShotTime = Main.time;
        for (int i = 0; i < 4; i++)
        {
            var projectile = this.ShootWith(player, source, position, velocity, type, damage, knockback,
                spread: 0.02f / Charge);
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.extraUpdates++;
        }

        return false;
    }
}