namespace WarframeMod.Content.Items.Accessories;

public class Intensify : ModItem
{
    public const float PERCENT_DAMAGE = 10f;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = 3;
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(gold: 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += PERCENT_DAMAGE / 100f;
    }
}