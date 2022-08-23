using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class ArcaneAvenger : ModItem
{
    public const int DAMAGE_TO_CRIT_RATIO = 2;
    public const int BUFF_DURATION = 720;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"When damaged: for every {DAMAGE_TO_CRIT_RATIO} points of damage taken receive +1% Critical Chance for {BUFF_DURATION / 60} seconds");
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.accessory = true;
        Item.rare = -12;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 4);
    }
    public override void UpdateInventory(Player player)
    {
        Item.rare = -12;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvengerPlayer>().enabled = true;
    }
}
class AvengerPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public int currentCritChance = 0;
    void ApplyBuff(int damage)
    {
        if (!enabled)
            return;
        currentCritChance = (int)MathF.Ceiling((float)damage / ArcaneAvenger.DAMAGE_TO_CRIT_RATIO);
        Player.AddBuff(ModContent.BuffType<ArcaneAvengerBuff>(), ArcaneAvenger.BUFF_DURATION);
    }
    public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
    {
        ApplyBuff((int)damage);
    }
}