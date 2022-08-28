using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class StradavarPrime : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Right Click to switch between Auto and Semi-auto fire modes\n+30% Critical Damage in Auto, +40% in Semi-auto\n70% Chance not to consume ammo in Auto");
    }
    int mode = 1;
    public int Mode // 0 is Auto, 1 is Semi
    {
        get => mode;
        set
        {
            if (value > 1) value = 0;
            mode = value;
            SetDefaults();
        }
    }
    public override void SetDefaults()
    {
        switch (Mode)
        {
            case 0:
                Item.damage = 24;
                Item.crit = 20;
                Item.useTime = 6;
                Item.useAnimation = 6;
                Item.autoReuse = true;
                break;
            default:
                Item.damage = 64;
                Item.crit = 26;
                Item.useTime = 12;
                Item.useAnimation = 12;
                Item.autoReuse = false;
                break;
        }
        Item.UseSound = SoundID.Item11;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = 40;
        Item.height = 13;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 2;
        Item.value = Item.buyPrice(gold: 6);
        Item.rare = ItemRarityID.LightPurple;
        Item.shoot = 10;
        Item.shootSpeed = 16f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Mode == 0 && Main.rand.Next(0, 100) < 70) return false;
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
        var projectile = this.ShootWith(player, source, position, velocity, type, damage, knockback, Mode == 0 ? 0.005f : 0.002f, Item.width);
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = 3;
        if (Mode == 1 && projectile.penetrate < 2 && projectile.penetrate != -1)
            projectile.penetrate = 2;
        projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = Mode == 0 ? 1.3f : 1.4f;

        return false;
    }
}