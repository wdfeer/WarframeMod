using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class KuvaKohm : ModItem
{
    public const int BLEED_CHANCE = 33;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);
    const int maxUseTime = 72;
    const int minUseTime = 14;
    const int maxMultishot = 5;
    public override void SetDefaults()
    {
        Item.damage = 30;
        Item.crit = 15;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 47;
        Item.height = 16;
        Item.useTime = maxUseTime;
        Item.useAnimation = maxUseTime;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1.5f;
        Item.value = Item.buyPrice(gold: 11);
        Item.rare = 9;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 14f;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = new SoundStyle("WarframeMod/Content/Sounds/KuvaKohmSound").ModifySoundStyle(pitchVariance: 0.15f);
    }
    int lastShotTime = 0;
    int timeSinceLastShot = 60;
    int multishot = 1;
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentVortex, 12);
        recipe.AddIngredient<Kuva>(5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
    public override bool CanUseItem(Player player)
    {
        timeSinceLastShot = (int)(Main.time - lastShotTime);

        if (timeSinceLastShot < 24)
            multishot = maxMultishot > multishot ? multishot + 1 : maxMultishot;
        else multishot = 1;

        if (Item.useTime > minUseTime)
        {
            Item.useTime = Item.useTime * 2 / 3;
            Item.useAnimation = Item.useTime;

            if (Item.useTime < minUseTime)
            {
                Item.useTime = minUseTime;
                Item.useAnimation = minUseTime;
            }
        }
        else if (timeSinceLastShot > minUseTime + 1)
        {
            Item.useTime += timeSinceLastShot / 4;
            Item.useAnimation += timeSinceLastShot / 4;
            if (Item.useTime > maxUseTime)
            {
                Item.useTime = maxUseTime;
                Item.useAnimation = maxUseTime;
            }
        }
        lastShotTime = (int)Main.time;

        return base.CanUseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, 0.6f);
        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width + 5);
        for (int i = 0; i < multishot; i++)
        {
            float spread = timeSinceLastShot > 46 ? 0.015f : 0.1f;
            var proj = Projectile.NewProjectileDirect(source, position, velocity.RotatedByRandom(spread), type, damage, knockback, player.whoAmI);
            proj.timeLeft = 120;
            proj.GetGlobalProjectile<FalloffGlobalProjectile>().SetFalloff(position, 16 * 36, 16 * 48, 0.6f);
            var buffProj = proj.GetGlobalProjectile<BuffGlobalProjectile>();
            buffProj.AddBleed(BLEED_CHANCE);
        }
        return false;
    }
}