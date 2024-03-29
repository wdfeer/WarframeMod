using Terraria.Localization;
using WarframeMod.Common.GlobalItems;

namespace WarframeMod.Content.Items.Weapons;

internal class Cassowar : CircularMelee
{
    public const int BLEED_CHANCE = 20;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 22;
        Item.crit = 2;
        Item.knockBack = 4.5f;
        Item.width = 60;
        Item.height = 60;
        Item.scale = 2.15f;
        Item.useTime = 27;
        Item.useAnimation = 27;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 1);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 0.7f;
    }
    public override float SizeMult => 1.6f;
    public override float UseSpeedMultiplier(Player player)
    {
        return SuperSwing ? 0.75f : 1f;
    }
    uint swings = 0;
    bool SuperSwing => swings % 3 == 0;
    public override bool CanUseItem(Player player)
    {
        swings++;
        if (SuperSwing)
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = 1f;
        else
            Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BLEED_CHANCE / 100f;
        return base.CanUseItem(player);
    }
    public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (SuperSwing)
        {
            modifiers.SourceDamage *= 2;
            modifiers.Knockback *= 1.75f;
        }
    }
    public override void ModifyHitPvp(Player player, Player target, ref Player.HurtModifiers modifiers)
    {
        if (SuperSwing)
            modifiers.SourceDamage *= 2;
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DemoniteBar, 10);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrimtaneBar, 10);
        recipe.AddIngredient(ItemID.TissueSample, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}