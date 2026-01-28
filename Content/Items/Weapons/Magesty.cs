using Terraria.Localization;
using WarframeMod.Common.GlobalItems;
using WarframeMod.Content.Projectiles;


namespace WarframeMod.Content.Items.Weapons;

internal class Magesty : CircularMelee
{
    public const int BLEED_CHANCE = 50;
    public const int CRIT_DAMAGE_BONUS_PERCENT = 25;

    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(BLEED_CHANCE, $"+{CRIT_DAMAGE_BONUS_PERCENT}%");

    public override void SetDefaults()
    {
        Item.damage = 120;
        Item.crit = 24;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.scale = 2.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(gold: 16);
        Item.GetGlobalItem<CritGlobalItem>().critMultiplier = 1f + CRIT_DAMAGE_BONUS_PERCENT / 100f;
        Item.GetGlobalItem<BleedingGlobalItem>().bleedingChance = BLEED_CHANCE / 100f;
    }

    public override void AddRecipes()
        => CreateRecipe().AddIngredient<OrthosPrime>()
            .AddIngredient(ItemID.ShroomiteBar, 12)
            .AddTile(TileID.MythrilAnvil)
            .Register();

    public override float SizeMult => 2f;

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