using Terraria.Localization;
using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class MotusSetup : MotusAccessory
{
    public const int RELATIVE_CRIT_PERCENT = CriticalDelay.RELATIVE_CRIT_PERCENT;
    public const int DURATION_SECONDS = 8;
    public override LocalizedText Tooltip =>
        base.Tooltip.WithFormatArgs(RELATIVE_CRIT_PERCENT, DURATION_SECONDS, KNOCKBACK_REDUCTION);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 1);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HarpyBanner);
        recipe.AddTile(TileID.Solidifier);
        recipe.Register();
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        base.UpdateAccessory(player, hideVisual);
        player.GetModPlayer<MotusSetupPlayer>().enabled = true;
    }
}

class MotusSetupPlayer : ModPlayer
{
    public bool enabled;

    public override void ResetEffects()
    {
        enabled = false;
    }

    public bool doubleJumpingOrWinging;

    public override void PostUpdate()
    {
        if (!enabled)
            return;
        bool touchingTiles = Player.TouchedTiles.Any();
        if (touchingTiles && doubleJumpingOrWinging)
        {
            Player.AddBuff(ModContent.BuffType<MotusSetupBuff>(), MotusSetup.DURATION_SECONDS * 60);
            doubleJumpingOrWinging = false;
        }
        else if (!doubleJumpingOrWinging)
        {
            doubleJumpingOrWinging = Player.ExtraJumps.ToArray().Any(x => x.Enabled && !x.Available);
        }
    }
}

class MotusSetupGlobalItem : GlobalItem
{
    public override bool WingUpdate(int wings, Player player, bool inUse)
    {
        if (inUse)
            player.GetModPlayer<MotusSetupPlayer>().doubleJumpingOrWinging = true;
        return false;
    }
}