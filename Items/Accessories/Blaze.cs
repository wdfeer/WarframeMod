using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod.Items.Accessories
{
    public class Blaze : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10% damage, +10% chance to set enemies on fire");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 44;
            Item.height = 64;
            Item.rare = 2;
            Item.value = Terraria.Item.sellPrice(gold: 1);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;

            BuffPlayer buffer = player.GetModPlayer<BuffPlayer>();
            buffer.OnNPCHit.Add(new BuffChance(BuffID.OnFire, 300, 0.1f));
        }
    }
}
