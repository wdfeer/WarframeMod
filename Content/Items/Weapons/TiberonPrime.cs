using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class TiberonPrime : ModItem
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AMMO_SAVE_CHANCE_AUTO);
    public const int AMMO_SAVE_CHANCE_AUTO = 75;
    FireMode mode = FireMode.Burst;
    public enum FireMode
    {
        Auto,
        Burst,
        Semi
    }
    public FireMode Mode
    {
        get => mode;
        set
        {
            if ((int)value > 2) value = 0;
            mode = value;
            UpdateStatsByFireMode();
        }
    }
    void UpdateStatsByFireMode()
    {
        switch (Mode)
        {
            case FireMode.Auto:
                Item.crit = 12;
                Item.useTime = 7;
                Item.useAnimation = 7;
                Item.autoReuse = true;
                break;
            case FireMode.Burst:
                Item.crit = 24;
                Item.useTime = 4;
                Item.useLimitPerAnimation = 3;
                Item.useAnimation = 20;
                Item.autoReuse = false;
                break;
            default:
                Item.crit = 26;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.autoReuse = false;
                break;
        }
    }
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 39;
        Item.height = 9;
        Item.scale = 1.1f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 8);
        Item.rare = 5;
        Item.shoot = 10;
        Item.shootSpeed = 17f;
        Item.useAmmo = AmmoID.Bullet;
        UpdateStatsByFireMode();
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HallowedBar, 16);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Mode == FireMode.Auto && Main.rand.Next(0, 100) < AMMO_SAVE_CHANCE_AUTO) return false;
        return base.CanConsumeAmmo(ammo, player);
    }
    public override bool AltFunctionUse(Player player)
        => true;
    int modeChangeTimer = 0;
    const int MODE_CHANGE_COOLDOWN = 20;
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
        SoundStyle soundStyle = new SoundStyle("WarframeMod/Content/Sounds/TiberonPrimeSound").ModifySoundStyle(pitchVariance: 0.1f);
        soundStyle.MaxInstances = 3;
        SoundEngine.PlaySound(soundStyle, position);

        WeaponCommon.ModifyProjectileSpawnPosition(ref position, velocity, Item.width);
        Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = -1;
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = Mode == FireMode.Auto ? 1.4f : Mode == FireMode.Burst ? 1.5f : 1.7f;
        return false;
    }
}