using Terraria.GameContent.Creative;
using WarframeMod.Common.GlobalItems;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Atterax : ModItem
{
	public const int BASE_CRIT_CHANCE = 21;
	public const float EXTRA_CRIT_MULT = 0.5f;
	public const float BLEED_CHANCE = 0.25f;
	public const float DMG_MULT_PER_MINION_SLOT = 0.33f;
	public override void SetDefaults()
	{
		Item.DefaultToWhip(ModContent.ProjectileType<AtteraxProjectile>(), 16, 3, 4.2f, 40);
		Item.crit = BASE_CRIT_CHANCE;
		Item.rare = 3;
		Item.GetGlobalItem<WhipRange>().extraRangeMult = 1.4f;
	}
	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		float freeSlots = player.maxMinions - player.slotsMinions;
		damage *= 1 + DMG_MULT_PER_MINION_SLOT * freeSlots;
	}
	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(ItemID.HellstoneBar, 5)
			.AddIngredient(ItemID.Chain, 2)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
