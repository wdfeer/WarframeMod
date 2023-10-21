using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class PiercingHit : ModItem
{
    public const float CHANCE = 0.12f;
    public const int DURATION = 360;
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(silver: 20);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BuffPlayer>().buffsOnHitNPC.Add(new BuffChance(BuffID.Weak, DURATION, CHANCE));
    }
}