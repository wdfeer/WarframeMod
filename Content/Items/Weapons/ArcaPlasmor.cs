using Terraria.DataStructures;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class ArcaPlasmor : ModItem
{
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/TenetArcaPlasmorSound").ModifySoundStyle(pitchVariance: 0.1f);
        Item.damage = 249;
        Item.crit = 18;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 13;
        Item.width = 48;
        Item.height = 16;
        Item.useTime = 54;
        Item.useAnimation = 54;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 7;
        Item.value = Item.sellPrice(gold: 3);
        Item.rare = 4;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<ArcaPlasmorProjectile>();
        Item.shootSpeed = 30f;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CobaltBar, 12);
        recipe.AddIngredient(ItemID.SoulofLight, 12);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PalladiumBar, 12);
        recipe.AddIngredient(ItemID.SoulofLight, 12);

        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var projectile = this.ShootWith(player, source, position, velocity, type, damage, knockback, spawnOffset: 40);
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 0.8f;
        projectile.GetGlobalProjectile<BuffGlobalProjectile>().buffChances.Add(new Common.BuffChance(BuffID.Confused, 300, 28));

        return false;
    }
}