using Terraria.DataStructures;
using WarframeMod.Common;
using WarframeMod.Common.GlobalItems;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class Zenistar : ModItem
{
    public const int DISK_DURATION = 900;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($@"Right click to throw a disk that throws shadowflames for {DISK_DURATION / 60} seconds
Inflicts Shadowflame and has 4x damage when the disk is not deployed
Guaranteed bleeding when the disk is deployed
Doubled benefit from Attack Speed");
    }
    public override void SetDefaults()
    {
        Item.damage = 45;
        Item.crit = 6;
        Item.knockBack = 1f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 48;
        Item.height = 46;
        Item.scale = 1.25f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 72;
        Item.useAnimation = 72;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 10);
        Item.shoot = ModContent.ProjectileType<ZenistarDisk>();
        Item.shootSpeed = 16f;
    }
    public override float UseSpeedMultiplier(Player player)
    {
        return player.GetAttackSpeed(DamageClass.Melee);
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public static Projectile FindDisk()
        => Main.projectile.FirstOrDefault(p => p.active && p.type == ModContent.ProjectileType<ZenistarDisk>() && p.owner == Main.myPlayer);
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        knockback /= 4;
        velocity /= player.GetAttackSpeed(DamageClass.Melee);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.altFunctionUse != 2)
            return false;
        Projectile disk = FindDisk();
        if (disk != null)
            disk.Kill();
        return true;
    }
    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2 || FindDisk() != null)
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = 1f;
        else
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = 0f;
        return true;
    }
    public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
    {
        if (FindDisk() == null)
        {
            damage *= 4;
            target.AddBuff(BuffID.ShadowFlame, 600);
        }
    }
}