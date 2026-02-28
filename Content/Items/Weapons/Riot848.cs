using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

public class Riot848 : ModItem
{
    public const int WEAK_CHANCE = 8;
    public const int MAGAZINE_SIZE = 16;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(WEAK_CHANCE, MAGAZINE_SIZE);

    public override void SetDefaults()
    {
        Item.damage = 51;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 17;
        Item.useTime = 8;
        Item.useAnimation = 8;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1f;
        Item.value = Item.sellPrice(gold: 18);
        Item.rare = ItemRarityID.Red;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Uzi);
        recipe.AddIngredient(ItemID.LunarBar, 10);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    private int shotCount;
    private bool Reloading => shotCount % (MAGAZINE_SIZE + 1) == MAGAZINE_SIZE;

    public override float UseSpeedMultiplier(Player player)
    {
        return Reloading ? 0.2f : 1f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        shotCount++;
        if (!Reloading)
        {
            // TODO: add custom shoot sound
            SoundEngine.PlaySound(SoundID.Item11.WithVolumeScale(0.7f), position);
            this.ShootWith(player, source, position, velocity, ModContent.ProjectileType<Riot848Projectile>(), damage,
                knockback, 0.02f, Item.width / 2f);
        }
        else
        {
            // TODO: reloading sound
            foreach (var proj in Main.projectile)
            {
                if (proj.owner == player.whoAmI && proj.ModProjectile is Riot848ImpaledProjectile modProj)
                {
                    // TODO: test whether this actually syncs the explosion properly
                    proj.netUpdate = true;
                    modProj.Explode(); 
                }
            }
        }

        return false;
    }
}