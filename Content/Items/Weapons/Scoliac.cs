using Terraria.Localization;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Scoliac : ModItem
{
	public const int BASE_CRIT_CHANCE = 9;
	public const int BLEED_CHANCE = 25;
	public const float DMG_MULT_PER_MINION_SLOT = 0.25f;
	public const int TAG_DAMAGE = 5;
	public const int TAG_TOXIN_CHANCE = 10;
	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(TAG_DAMAGE, TAG_TOXIN_CHANCE, BLEED_CHANCE);
	public override void SetDefaults()
	{
		Item.DefaultToWhip(ModContent.ProjectileType<ScoliacProjectile>(), 16, 4f, 5f, 35);
		Item.crit = BASE_CRIT_CHANCE;
		Item.rare = 4;
		Item.value = Item.sellPrice(gold: 4);
	}
	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		damage *= 1 + DMG_MULT_PER_MINION_SLOT * player.maxMinions;
	}
	public override void AddRecipes()
		=> CreateRecipe().AddIngredient(ItemID.ChlorophyteBar, 12)
				   .AddIngredient(ItemID.SpiderFang, 6)
				   .AddTile(TileID.MythrilAnvil)
				   .Register();
	public override bool MeleePrefix()
		=> true;
}
