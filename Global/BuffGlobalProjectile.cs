using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using WarframeMod.Global;

namespace WarframeMod.Projectiles
{
    internal class BuffGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public float bleedingChance = 0;
        public List<BuffChance> buffChances = new List<BuffChance>();
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            BuffChance.ApplyBuffs(target, buffChances);
            if (Main.rand.NextFloat() < bleedingChance)
            {
                var bleedNPC = target.GetGlobalNPC<BleedingGlobalNPC>();
                bleedNPC.bleeds.Add(new BleedingBuff(damage / 5, 300));
            }
        }
    }
}
