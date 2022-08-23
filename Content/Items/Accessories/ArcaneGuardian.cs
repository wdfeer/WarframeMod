using WarframeMod.Content.Buffs;

namespace WarframeMod.Content.Items.Accessories;

public class ArcaneGuardian : ModItem
{
    public const int DAMAGE_TO_DEFENSE_RATIO = 5;
    public const int BUFF_DURATION = 300;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"When damaged: for every {DAMAGE_TO_DEFENSE_RATIO} points of damage taken receive +1 Defense for {BUFF_DURATION / 60} seconds");
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
        player.GetModPlayer<GuardianPlayer>().enabled = true;
    }
}
class GuardianPlayer : ModPlayer
{
    public bool enabled;
    public override void ResetEffects()
        => enabled = false;
    public int currentDefense = 0;
    void ApplyBuff(int damage)
    {
        if (!enabled)
            return;
        currentDefense = (int)MathF.Ceiling((float)damage / ArcaneGuardian.DAMAGE_TO_DEFENSE_RATIO);
        Player.AddBuff(ModContent.BuffType<ArcaneGuardianBuff>(), ArcaneGuardian.BUFF_DURATION);
    }
    public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
    {
        ApplyBuff((int)damage);
    }
}