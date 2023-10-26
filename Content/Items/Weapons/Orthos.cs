using Terraria.Localization;
using WarframeMod.Common.GlobalItems;

namespace WarframeMod.Content.Items.Weapons;

internal class Orthos : CircularMelee
{
    public const int BLEED_CHANCE = 15;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE);
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 13;
        Item.crit = 2;
        Item.knockBack = 3.5f;
        Item.width = 47;
        Item.height = 48;
        Item.scale = 2.5f;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.rare = 1;
        Item.value = Item.sellPrice(silver: 50);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 0.75f;
        Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BLEED_CHANCE / 100f;
    }
    public override float SizeMult => 2f;
    public override float UseSpeedMultiplier(Player player)
    {
        return SuperSwing ? 0.8f : 1f;
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
            modifiers.Knockback *= 2;
        }
    }
    public override void ModifyHitPvp(Player player, Player target, ref Player.HurtModifiers modifiers)
    {
        if (SuperSwing)
            modifiers.SourceDamage *= 2;
    }
    public override void AddRecipes()
        => CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar, 20)
                         .AddTile(TileID.Anvils)
                         .Register();
}