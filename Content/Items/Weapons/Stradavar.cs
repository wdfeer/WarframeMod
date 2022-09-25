using Terraria.Audio;
using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;
public class Stradavar : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Right Click to switch between Auto and Semi-auto fire modes\n50% Chance not to consume ammo in Auto");
    }
    int mode = 1;
    public int Mode // 0 is Auto, 1 is Semi
    {
        get => mode;
        set
        {
            if (value > 1) value = 0;
            mode = value;
            SetModeDefaults();
        }
    }
    void SetModeDefaults()
    {
        switch (Mode)
        {
            case 0:
                Item.damage = 5;
                Item.crit = 20;
                Item.useTime = 6;
                Item.useAnimation = 6;
                Item.autoReuse = true;
                break;
            default:
                Item.damage = 14;
                Item.crit = 24;
                Item.useTime = 12;
                Item.useAnimation = 12;
                Item.autoReuse = false;
                break;
        }
    }
    public override void SetDefaults()
    {
        SetModeDefaults();
        Item.UseSound = SoundID.Item11;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 40;
        Item.height = 12;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 2;
        Item.value = Item.buyPrice(gold: 6);
        Item.rare = 2;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Minishark, 1);
        recipe.AddIngredient(ItemID.Revolver, 1);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Mode == 0 && Main.rand.Next(0, 100) < 50) return false;
        return base.CanConsumeAmmo(ammo, player);
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    int modeChangeTimer = 0;
    const int MODE_CHANGE_COOLDOWN = 30;
    public override void UpdateInventory(Player player)
        => modeChangeTimer++;
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            if (modeChangeTimer > MODE_CHANGE_COOLDOWN)
            {
                Mode++;
                modeChangeTimer = 0;
                SoundEngine.PlaySound(SoundID.Unlock);
            }
            return false;
        }
        return base.CanUseItem(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, Mode == 0 ? 0.01f : 0.005f, Item.width);
        proj.usesLocalNPCImmunity = true;
        proj.localNPCHitCooldown = 3;

        return false;
    }
}