using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Lecta : ModItem
{
	public const float ELECTRO_CHANCE = 0.5f;
	public const float DMG_MULT_PER_MINION_SLOT = 0.2f;
	public override void SetDefaults()
	{
		Item.DefaultToWhip(ModContent.ProjectileType<LectaProjectile>(), 60, 2f, 4.25f, 50);
		Item.crit = 1;
		Item.rare = 4;
		Item.value = Item.sellPrice(gold: 3);
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
	public override bool MeleePrefix()
		=> true;
}
