using Terraria.DataStructures;
using WarframeMod.Common.GlobalItems;
using WarframeMod.Common.Players;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;

internal class Zenistar : ModItem
{
    public const int DISK_DURATION = 720;
    public override void SetDefaults()
    {
        Item.damage = 45;
        Item.crit = 6;
        Item.knockBack = 1f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 48;
        Item.height = 46;
        Item.scale = 1.75f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 43;
        Item.useAnimation = 43;
        Item.rare = 4;
        Item.value = Item.sellPrice(gold: 10);
        Item.shoot = ModContent.ProjectileType<ZenistarDisk>();
        Item.shootSpeed = 16f;
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
        damage = (int)(damage * player.GetAttackSpeed(DamageClass.Melee));
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (player.altFunctionUse != 2)
            return false;
        Projectile disk = FindDisk();
        if (disk != null)
            disk.Kill();
        disk = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, player.GetModPlayer<TrueMeleeRangePlayer>().absoluteExtraRange);
        return false;
    }
    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2 || FindDisk() != null)
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = 1f;
        else
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = 0f;
        return true;
    }
    public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (FindDisk() == null)
        {
            modifiers.SourceDamage *= 4;
            target.AddBuff(BuffID.ShadowFlame, 600);
        }
    }
}