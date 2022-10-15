using Terraria.GameContent.Creative;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Lecta : ModItem
{
	public const int BASE_CRIT_CHANCE = 1;
	public const float ELECTRO_CHANCE = 0.5f;
	public const float DMG_MULT_PER_MINION_SLOT = 0.2f;
	public override void SetStaticDefaults() {
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		Tooltip.SetDefault($@"Your summons will focus struck enemies
{ELECTRO_CHANCE * 100}% electricity chance
-25% Critical damage
Damage is increased by {DMG_MULT_PER_MINION_SLOT * 100}% for each empty minion slot
Increased benefit from Attack Speed");
	}
	public override void SetDefaults() {
		Item.DefaultToWhip(ModContent.ProjectileType<LectaProjectile>(), 60, 2f, 4.25f, 50);
		Item.crit = BASE_CRIT_CHANCE;
		Item.rare = 4;
	}
	public override float UseSpeedMultiplier(Player player)
	{
		return MathF.Sqrt(player.GetAttackSpeed(DamageClass.Melee));
	}
	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		float freeSlots = player.maxMinions - player.slotsMinions;
        damage *= 1 + DMG_MULT_PER_MINION_SLOT * freeSlots;
	}
}
