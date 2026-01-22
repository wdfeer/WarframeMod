using Terraria.DataStructures;
using Terraria.Localization;
using WarframeMod.Content.Projectiles;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Weapons;
public class Grimoire : ModItem
{
    public const int ELECTRO_CHANCE = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ELECTRO_CHANCE);
    public override void SetDefaults()
    {
        Item.damage = 12;
        Item.crit = 16;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 6;
        Item.width = 31;
        Item.height = 31;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2.25f;
        Item.value = Item.sellPrice(gold: 3);
        Item.rare = 3;
        Item.shoot = ModContent.ProjectileType<GrimoireProjectile>();
        Item.shootSpeed = 20f;
        Item.UseSound = SoundID.Item11;
    }
    public override bool AltFunctionUse(Player player)
        => true;

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
        ref float knockback)
    {
        if (player.altFunctionUse == 2)
        {
            type = ModContent.ProjectileType<GrimoireAltProjectile>();
            velocity /= 2;
        }
    }
}