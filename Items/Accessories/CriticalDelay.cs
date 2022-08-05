using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Players;

namespace WarframeMod.Items.Accessories
{
    public class CriticalDelay : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+90% Crit Chance relative to the weapon's base crit chance, but -15% Fire Rate");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 44;
            Item.height = 64;
            Item.rare = 2;
            Item.value = Terraria.Item.sellPrice(gold: 3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.HeldItem.damage > 0)
                player.GetCritChance(DamageClass.Generic) += (player.HeldItem.crit + 4) * 0.9f;
            player.GetModPlayer<FireRatePlayer>().FireRateMultiplier -= 0.15f;
        }
    }
}
