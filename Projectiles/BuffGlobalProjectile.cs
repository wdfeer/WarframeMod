using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WarframeMod.Projectiles
{
    internal class BuffGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public List<BuffChance> buffChances = new List<BuffChance>();
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
            => BuffChance.ApplyBuffs(target, buffChances);
    }
}
