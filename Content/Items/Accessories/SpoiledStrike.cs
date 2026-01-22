namespace WarframeMod.Content.Items.Accessories;

public class SpoiledStrike : ModItem
{
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 3;
        Item.value = Item.sellPrice(gold: 2);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.18f;
        player.GetAttackSpeed(DamageClass.Generic) -= 0.09f;
    }
}
