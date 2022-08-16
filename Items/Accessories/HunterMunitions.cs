using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Global;
using WarframeMod.Players;

namespace WarframeMod.Items.Accessories
{
    public class HunterMunitions : ModItem
    {
        public const int bleedChance = 30;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault($"{bleedChance}% bleeding chance on critical hits");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 44;
            Item.height = 64;
            Item.rare = 2;
            Item.value = Terraria.Item.sellPrice(silver: 75);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HunterMunitionsPlayer>().enabled = true;
        }
    }
    internal class HunterMunitionsPlayer : ModPlayer
    {
        public bool enabled = false;
        public override void ResetEffects()
        {
            enabled = false;
        }
        public void TryBleed(NPC target, int damageAfterCrit)
        {
            if (enabled && Main.rand.NextFloat() < HunterMunitions.bleedChance / 100f)
            {
                ref var bleeds = ref target.GetGlobalNPC<BleedingGlobalNPC>().bleeds;
                bleeds.Add(new BleedingBuff(damageAfterCrit / 5, 300));
            }
        }
    }
}
