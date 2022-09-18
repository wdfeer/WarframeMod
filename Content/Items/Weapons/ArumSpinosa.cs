using Terraria.Audio;
using Terraria.DataStructures;

namespace WarframeMod.Content.Items.Weapons;

public class ArumSpinosa : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Throws 6 projectiles that inflict Venom and have a 40% Bleed chance");
    }
    public override void SetDefaults()
    {
        Item.damage = 37;
        Item.crit = 5;
        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = 48;
        Item.height = 43;
        Item.useTime = 48;
        Item.useAnimation = 48;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 4);
        Item.rare = 5;
        Item.shoot = ModContent.ProjectileType<Projectiles.ArumSpinosaProjectile>();
        Item.UseSound = SoundID.Item39.WithVolumeScale(0.4f);
        Item.autoReuse = true;
        Item.shootSpeed = 16f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ChlorophyteBar, 7);
        recipe.AddIngredient(ItemID.SpiderFang, 12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float spreadInDegrees = 22;
        velocity = velocity.RotatedBy(MathHelper.ToRadians(spreadInDegrees / 2));
        for (int i = 0; i < 6; i++)
        {
            var proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            velocity = velocity.RotatedBy(MathHelper.ToRadians(-spreadInDegrees / 6));
        }
        SoundEngine.PlaySound(SoundID.Item1, position);

        return false;
    }
}