using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class NaturalTalent : ModItem
{
    public const int MAGIC_USE_SPEED_PERCENT = 15;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{MAGIC_USE_SPEED_PERCENT}% magic weapon use speed");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 4);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetAttackSpeed(DamageClass.Magic) += MAGIC_USE_SPEED_PERCENT / 100f;
    }
}
