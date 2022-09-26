using WarframeMod.Common;
using WarframeMod.Common.Players;

namespace WarframeMod.Content.Items.Accessories;

public class Blaze : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("+8% damage, +24% chance to set enemies on fire");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 2;
        Item.value = Item.sellPrice(gold: 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.08f;

        BuffPlayer buffman = player.GetModPlayer<BuffPlayer>();
        buffman.onHitNPC.Add(new BuffChance(BuffID.OnFire, 300, 0.24f));
    }
}
