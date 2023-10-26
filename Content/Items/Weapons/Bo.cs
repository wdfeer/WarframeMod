using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;

internal class Bo : CircularMelee
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DEFENSE_PENETRATION);
    public const int DEFENSE_PENETRATION = 25;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.damage = 20;
        Item.crit = 8;
        Item.knockBack = 4.75f;
        Item.width = 57;
        Item.height = 60;
        Item.scale = 2.25f;
        Item.useTime = 28;
        Item.useAnimation = 28;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 33);
    }
    public override float SizeMult => 1.8f;
    public override float UseSpeedMultiplier(Player player)
    {
        return SuperSwing ? 0.7f : 1f;
    }
    uint swings = 0;
    bool SuperSwing => swings % 3 == 0;
    public override bool CanUseItem(Player player)
    {
        swings++;
        if (SuperSwing)
            Item.ArmorPenetration = DEFENSE_PENETRATION;
        else
            Item.ArmorPenetration = 0;
        return base.CanUseItem(player);
    }
    public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
    {
        if (SuperSwing)
        {
            modifiers.SourceDamage *= 3;
            modifiers.Knockback *= 2;
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
        recipe.AddIngredient(ModContent.ItemType<MK1Bo>());
        recipe.AddIngredient(ItemID.DemoniteBar, 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

        recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<MK1Bo>());
        recipe.AddIngredient(ItemID.CrimtaneBar, 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}