using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod
{
    internal class CritsPlayer : ModPlayer
    {
        int GetCritLevel(int critChance)
        {
            int lvl = 0;
            while (critChance > 0)
            {
                if (Main.rand.Next(0, 101) < critChance)
                    lvl++;
                critChance -= 100;
            }
            return lvl;
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            var critLevel = GetCritLevel(Player.GetWeaponCrit(item));
            if (crit && critLevel > 0)
            {
                damage = (int)(damage * Math.Pow(2, critLevel - 1));
                OverCritVisuals(target, knockback);
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            var critLevel = GetCritLevel(proj.CritChance);
            if (crit && critLevel > 0)
            {
                damage = (int)(damage * Math.Pow(2 * proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier, critLevel - 1));
                OverCritVisuals(target, knockback);
            }
        }
        void OverCritVisuals(NPC target, float knockback)
        {
            for (int i = 0; i < 1 + knockback / 2; i++)
            {
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Clentaminator_Red);
                dust.scale = knockback / 8 + 0.2f;
            }
        }
    }
    internal class CritGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public float CritMultiplier { get; set; } = 1f;
    }
}
