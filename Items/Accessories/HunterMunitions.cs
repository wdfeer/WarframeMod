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
            player.GetModPlayer<HunterMunitionsPlayer>().hunterMunitions = true;
        }
    }
    internal class HunterMunitionsPlayer : ModPlayer
    {
        public bool hunterMunitions = false;
        public override void ResetEffects()
        {
            hunterMunitions = false;
        }
        void RollAndApplyBleed(NPC target, int damage, bool crit)
        {
            if (crit && Main.rand.NextFloat() < HunterMunitions.bleedChance / 100f)
            {
                ref var bleeds = ref target.GetGlobalNPC<BleedingGlobalNPC>().bleeds;
                bleeds.Add(new BleedingBuff(damage * 2 / 5, 300));
            }
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (hunterMunitions)
                RollAndApplyBleed(target, damage, crit);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (hunterMunitions)
                RollAndApplyBleed(target, damage, crit);
        }
    }
}
