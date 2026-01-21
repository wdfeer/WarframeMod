using WarframeMod.Common;
using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Items.Accessories;

public class BulletDance : ModItem
{
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(gold: 8);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<BulletDancePlayer>().active = true;
    }
}

class BulletDancePlayer : ModPlayer
{
    public bool active;
    public override void ResetEffects()
    {
        active = false;
    }

    private int[] gunblades = [ModContent.ItemType<Redeemer>(), ModContent.ItemType<Sarpa>(), ModContent.ItemType<RedeemerPrime>()];
    public override float UseSpeedMultiplier(Item item)
    {
        if (active && gunblades.Contains(item.type))
        {
            return 1.5f;
        }
        return base.UseSpeedMultiplier(item);
    }
}
