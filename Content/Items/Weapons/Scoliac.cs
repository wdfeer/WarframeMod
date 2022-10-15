using Terraria.GameContent.Creative;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Scoliac : ModItem
{
	public const int BASE_CRIT_CHANCE = 9;
	public const float BLEED_CHANCE = 0.25f;
	public const float DMG_MULT_PER_MINION_SLOT = 0.25f;
	public const int TAG_DAMAGE = 10;
    public const int TAG_POISON_CHANCE = 50;
    public const int TAG_VENOM_CHANCE = 20;
	public override void SetStaticDefaults() {
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		Tooltip.SetDefault($@"Your summons will focus struck enemies
{TAG_DAMAGE} summon tag damage
{TAG_POISON_CHANCE}% summon tag poison chance
{TAG_VENOM_CHANCE}% summon tag venom chance
{(int)(BLEED_CHANCE * 100)}% bleeding chance
-25% Critical damage
Damage is increased by {DMG_MULT_PER_MINION_SLOT * 100}% multiplied by the number of max minion slots");
	}
	public override void SetDefaults() {
		Item.DefaultToWhip(ModContent.ProjectileType<ScoliacProjectile>(), 16, 4f, 5f, 35);
		Item.crit = BASE_CRIT_CHANCE;
		Item.rare = 4;
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
}
