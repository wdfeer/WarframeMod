using Terraria.GameContent.UI.States;
using Terraria.Localization;
using WarframeMod.Common;
using WarframeMod.Common.GlobalItems;
using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Content.Items.Weapons;

internal class Cadus : CircularMelee
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ELECTRO_CHANCE);
    public const int ELECTRO_CHANCE = 15;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 40;
        Item.crit = 11;
        Item.knockBack = 8f;
        Item.width = 59;
        Item.height = 60;
        Item.scale = 2.25f;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(silver: 75);
    }
    public override float SizeMult => 1.8f;
    public override float UseSpeedMultiplier(Player player)
    {
        return SuperSwing ? 0.7f : 1f;
    }
    uint swings = 0;
    bool SuperSwing => swings % 3 == 0;
    public override void UseAnimation(Player player)
    {
        swings++;
        base.UseAnimation(player);
    }
    public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (SuperSwing)
        {
            modifiers.SourceDamage *= 3;
            modifiers.Knockback *= 2;
        }
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Random.Shared.NextInt64(1, 101) < ELECTRO_CHANCE)
        {
            ElectricityBuff.Create(damageDone, target);
        }
    }

    public override void ModifyHitPvp(Player player, Player target, ref Player.HurtModifiers modifiers)
    {
        if (SuperSwing)
            modifiers.SourceDamage *= 3;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<Bo>());
        recipe.AddIngredient(ItemID.Wire, 30);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}