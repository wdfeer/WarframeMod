using Terraria.Audio;
using Terraria.DataStructures;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class TenetDetron : ModItem
{
    public const int FALLOFF_START = 40;
    public const int FALLOFF_MAX = 70;
    public const float MAX_FALLOFF_DAMAGE_DECREASE = 0.55f;
    public const int MULTISHOT = 10;
    public override void SetDefaults()
    {
        Item.damage = 60;
        Item.crit = 14;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 33;
        Item.width = 32;
        Item.height = 16;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 2.5f;
        Item.value = Item.buyPrice(gold: 75);
        Item.rare = 10;
        Item.shoot = ProjectileID.LaserMachinegunLaser;
        Item.shootSpeed = 16;
        Item.autoReuse = true;
        Item.UseSound = new SoundStyle("WarframeMod/Content/Sounds/MaraDetronSound").ModifySoundStyle(pitchVariance: 0.05f);
    }
    public override bool AltFunctionUse(Player player)
        => true;
    public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
    {
        if (player.altFunctionUse == 2)
            mult = 6;
    }
    public override float UseTimeMultiplier(Player player)
    {
        if (player.altFunctionUse == 2)
            return 0.677f;
        return base.UseTimeMultiplier(player);
    }
    public override float UseAnimationMultiplier(Player player)
    {
        if (player.altFunctionUse == 2)
            return 3.8f;
        return base.UseAnimationMultiplier(player);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item91.ModifySoundStyle(pitchVariance: 0.1f), position);
        for (int i = 0; i < MULTISHOT; i++)
        {
            Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback, 0.08f, Item.width);
            proj.GetGlobalProjectile<BuffGlobalProjectile>().buffChances.Add(new Common.BuffChance(BuffID.Confused, 120, 0.1f));
            proj.GetGlobalProjectile<FalloffGlobalProjectile>().SetFalloff(position, FALLOFF_START, FALLOFF_MAX, MAX_FALLOFF_DAMAGE_DECREASE);
        }

        return false;
    }
}