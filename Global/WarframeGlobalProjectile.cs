using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WarframeMod.Global
{
    internal class WarframeGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public Func<Projectile, int, Entity, int> modifyDamage = (proj, oldDamage, target) => oldDamage;
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = modifyDamage(projectile, damage, target);
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref int damage, ref bool crit)
        {
            damage = modifyDamage(projectile, damage, target);
        }
        public override void ModifyHitPvp(Projectile projectile, Player target, ref int damage, ref bool crit)
        {
            damage = modifyDamage(projectile, damage, target);
        }
    }
}
