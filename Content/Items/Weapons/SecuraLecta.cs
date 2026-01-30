using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class SecuraLecta : ModItem
{
	public const float DMG_MULT_PER_MINION_SLOT = 0.2f;
    public const int TAG_ELECTRO_CHANCE = 20;
	public override void SetDefaults()
	{
		Item.DefaultToWhip(ModContent.ProjectileType<SecuraLectaProjectile>(),
            100,
            3f,
            8f,
            28);
		Item.crit = 11;
		Item.rare = 7;
		Item.value = Item.sellPrice(gold: 18);
	}

    public override void AddRecipes()
        => CreateRecipe()
            .AddIngredient<Lecta>()
            .AddIngredient<Fieldron>()
            .AddTile(TileID.MythrilAnvil)
            .Register();

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		float freeSlots = player.maxMinions - player.slotsMinions;
		damage *= 1 + DMG_MULT_PER_MINION_SLOT * freeSlots;
	}
    
	public override bool MeleePrefix()
		=> true;
}
