using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Supra : ModItem
{
    public override void SetDefaults()
    {
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/SupraVandalSound").ModifySoundStyle(pitchVariance: 0.1f);
        Item.damage = 61;
        Item.crit = 8;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 2;
        Item.width = 17;
        Item.height = 48;
        Item.useTime = BaseUseTime;
        Item.useAnimation = BaseUseTime;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2;
        Item.value = Item.buyPrice(gold: 5);
        Item.rare = 9;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.MartianWalkerLaser;
        Item.shootSpeed = 16f;
    }
    protected int lastShotTime = 0;
    protected int timeSinceLastShot = 60;
    protected virtual int BaseUseTime => 16;
    public override bool CanUseItem(Player player)
    {
        timeSinceLastShot = (int)Main.time - lastShotTime;
        if (Item.useTime > 5)
        {
            Item.useTime -= 3;
            Item.useAnimation -= 3;
            if (Item.useTime < 5)
            {
                Item.useTime = 5;
                Item.useAnimation = 5;
            }
        }
        else if (timeSinceLastShot >= BaseUseTime)
        {
            Item.useTime += timeSinceLastShot / 3;
            Item.useAnimation += timeSinceLastShot / 3;
            if (Item.useTime > BaseUseTime)
            {
                Item.useTime = BaseUseTime;
                Item.useAnimation = BaseUseTime;
            }
        }
        lastShotTime = (int)Main.time;

        return base.CanUseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity * 4, ProjectileID.LaserMachinegunLaser, damage, knockback, timeSinceLastShot > 30 ? 0 : 0.065f, 50);
        proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = 0.9f;
        return false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LaserMachinegun);
        recipe.AddIngredient(ModContent.ItemType<Fieldron>(), 1);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}