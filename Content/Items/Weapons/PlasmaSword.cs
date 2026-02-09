using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Common.GlobalProjectiles;


namespace WarframeMod.Content.Items.Weapons;

public class PlasmaSword : ModItem
{
    public const int BLEED_CHANCE = 50;
    public const int ELECTRO_CHANCE = 50;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE, ELECTRO_CHANCE);

    public override void SetDefaults()
    {
        Item.damage = 24;
        Item.crit = 14;
        Item.knockBack = 1f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 32;
        Item.height = 30;
        Item.scale = 1.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(gold: 16);
        Item.shoot = ProjectileID.SwordBeam;
        Item.shootSpeed = 8f;
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        BleedingBuff.Create(damageDone, target);
        ElectricityBuff.Create(damageDone, target);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        var proj = this.ShootWith(player, source, position, velocity, type, damage, knockback);
        var buffProj = proj.GetGlobalProjectile<BuffGlobalProjectile>();
        buffProj.AddBleed(BLEED_CHANCE);
        buffProj.AddElectro(ELECTRO_CHANCE);
        return false;
    }
}