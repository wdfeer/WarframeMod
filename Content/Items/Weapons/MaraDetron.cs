using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Content.Items.Weapons;

public class MaraDetron : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots 7 lasers at once\n-25% Critical Damage\n14% chance to confuse enemies");
    }
    public override void SetDefaults()
    {
        Item.damage = 29;
        Item.crit = 4;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 24;
        Item.width = 32;
        Item.height = 16;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 12);
        Item.rare = 5;
        Item.UseSound = SoundID.Item91.ModifySoundStyle(pitchVariance: 0.1f);
        Item.shoot = ProjectileID.LaserMachinegunLaser;
        Item.shootSpeed = 16;
        Item.autoReuse = true;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Detron>());
        recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int i = 0; i < 7; i++)
        {
            Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.1f, Item.width);
            proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 0.75f;
            proj.GetGlobalProjectile<BuffGlobalProjectile>().buffChances.Add(new Common.BuffChance(BuffID.Confused, 180, 0.14f));
        }

        return false;
    }
}