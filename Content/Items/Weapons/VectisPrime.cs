using Terraria.Audio;
using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;
public class VectisPrime : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots a high-velocity bullet that penetrates through 2 enemies\nHas to reload after every second shot");
    }
    public override void SetDefaults()
    {
        Item.damage = 225;
        Item.crit = 26;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 63;
        Item.height = 19;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 8.5f;
        Item.value = Item.sellPrice(gold: 5);
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 16f;
        Item.shoot = 10;
        Item.useAmmo = AmmoID.Bullet;
    }
    bool reloading = false;
    string SoundPath => "WarframeMod/Content/Sounds/" + (reloading ? "VectisPrimeSound2" : "VectisPrimeSound1");
    public override bool CanUseItem(Player player)
    {
        if (reloading)
        {
            Item.useTime = 66;
            Item.useAnimation = 66;
        }
        else
        {
            Item.useTime = 40;
            Item.useAnimation = 40;
        }
        return true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(new SoundStyle(SoundPath).ModifySoundStyle(), position);

        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, spawnOffset: Item.width - 3);
        (Mod as WarframeMod).SetProjectileExtraUpdatesNetSafe(proj, proj.extraUpdates + 3);
        proj.penetrate = 2;
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = -1;

        reloading = !reloading;

        return false;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Vectis>());
        recipe.AddIngredient(ItemID.SniperRifle);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}