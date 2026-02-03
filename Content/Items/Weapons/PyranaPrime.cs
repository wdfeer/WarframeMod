using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using WarframeMod.Common.GlobalProjectiles;

namespace WarframeMod.Content.Items.Weapons;

public class PyranaPrime : ModItem
{
    public const int MULTISHOT = 7;
    public const int FALLOFF_START = 15;
    public const int FALLOFF_MAX = 30;
    public const float MAX_FALLOFF_DAMAGE_DECREASE = 0.5f;
    public override void SetDefaults()
    {
        Item.damage = 14;
        Item.crit = 20;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 32;
        Item.height = 16;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 1.3f;
        Item.value = Item.sellPrice(gold: 27);
        Item.rare = 5;
        Item.shootSpeed = 16;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.useAmmo = AmmoID.Bullet;
        Item.UseSound = new Terraria.Audio.SoundStyle("WarframeMod/Content/Sounds/PyranaPrimeSound").ModifySoundStyle(pitchVariance: 0.1f);
    }

    public override void AddRecipes()
        => CreateRecipe()
            .AddIngredient<Pyrana>()
            .AddIngredient(ItemID.HallowedBar, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        this.ModifyAmmoDamage(player, ref damage, 0.5f);
        for (int i = 0; i < MULTISHOT; i++)
        {
            Projectile proj = this.ShootWith(player, source, position, velocity, type, damage, knockback,
                MathF.PI * 16.5f / 180, Item.width);
            proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier += 0.1f;
            proj.GetGlobalProjectile<FalloffGlobalProjectile>()
                .SetFalloff(position, FALLOFF_START, FALLOFF_MAX, MAX_FALLOFF_DAMAGE_DECREASE);
        }

        return false;
    }
}