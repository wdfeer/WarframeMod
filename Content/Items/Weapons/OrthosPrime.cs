using Terraria.Localization;
using WarframeMod.Common.GlobalItems;
using Terraria.ID;


namespace WarframeMod.Content.Items.Weapons;

internal class OrthosPrime : CircularMelee
{
    public const int BLEED_CHANCE = 36;
    public const int CRIT_DAMAGE_BONUS_PERCENT = 10;
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BLEED_CHANCE, $"+{CRIT_DAMAGE_BONUS_PERCENT}%");
    public override void SetDefaults()
    {
        Item.damage = 70;
        Item.crit = 20;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 47;
        Item.height = 48;
        Item.scale = 2.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.buyPrice(gold: 8);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 1f + CRIT_DAMAGE_BONUS_PERCENT / 100f;
        Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BLEED_CHANCE / 100f;
    }
    public override void AddRecipes()
        => CreateRecipe().AddIngredient(ItemID.HallowedBar, 16)
                         .AddTile(TileID.MythrilAnvil)
                         .Register();

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
}