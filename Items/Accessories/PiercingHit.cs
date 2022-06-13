using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Players;

namespace WarframeMod.Items.Accessories
{

    public class PiercingHit : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10% Chance to inflict the Weakened debuff for 9 seconds");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 44;
            Item.height = 64;
            Item.rare = 2;
            Item.value = Terraria.Item.sellPrice(silver: 20);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BuffPlayer>().OnNPCHit.Add(new BuffChance(BuffID.Weak, 540, 0.1f));
        }
    }
}