﻿using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Weapons;
public class Atterax : ModItem
{
	public const float BASE_CRIT_CHANCE_PERCENT = 25f;
	public const float EXTRA_CRIT_MULT = 0.5f;
	public const float BLEED_CHANCE = 0.5f;
	public const float DMG_MULT_PER_MINION_SLOT = 0.25f;
	public override void SetStaticDefaults() {
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		Tooltip.SetDefault($@"Your summons will focus struck enemies
{BLEED_CHANCE * 100}% bleeding chance
+{EXTRA_CRIT_MULT * 100}% Critical Damage
Damage is increased by {DMG_MULT_PER_MINION_SLOT * 100}% for each empty minion slot
Affected by Reach and Primed Reach
Damage is not decreased by the number of enemies hit");
	}
	public static int GetVisualCrit(Player player)
	{
        var critter = player.GetModPlayer<CritPlayer>();
        return (int)(GetRealCrit(player) + critter.summonCritChance);
    }
	public static int GetRealCrit(Player player)
	{
        var critter = player.GetModPlayer<CritPlayer>();
        return (int)(BASE_CRIT_CHANCE_PERCENT * critter.BaseCritChanceMult + player.GetCritChance(DamageClass.Generic));
    }
	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		if (tooltips.Any(t => t.Name == "CritChance"))
			return;
		int crit = GetVisualCrit(Main.LocalPlayer);
		tooltips.Insert(tooltips.FindIndex(t => t.Name == "Damage") + 1, new TooltipLine(Mod, "CritChance", $"{crit}% critical strike chance"));
	}
	public override void SetDefaults() {
		Item.DefaultToWhip(ModContent.ProjectileType<AtteraxProjectile>(), 16, 3, 4, 40); 
		Item.shootSpeed = 3.5f;
		Item.rare = 3;
	}
	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		float freeSlots = (player.maxMinions - player.slotsMinions);
        damage *= 1 + DMG_MULT_PER_MINION_SLOT * freeSlots;
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		var rangePl = player.GetModPlayer<TrueMeleeRangePlayer>();
		float rangeMult = rangePl.rangeMult + rangePl.absoluteExtraRange / 240f;
		if (rangeMult > 2f)
			rangeMult = 2f;
		velocity *= rangeMult;
	}
	public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ItemID.DemoniteBar, 5)
            .AddIngredient(ItemID.Chain, 8)
            .AddTile(TileID.Anvils)
            .Register();
        CreateRecipe().AddIngredient(ItemID.CrimtaneBar, 5)
            .AddIngredient(ItemID.Chain, 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
