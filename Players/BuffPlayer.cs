using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WarframeMod.Players
{
    public class BuffPlayer : ModPlayer
    {
        public List<BuffChance> OnNPCHit;
        public override void ResetEffects()
        {
            OnNPCHit = new List<BuffChance>();
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            BuffChance.ApplyBuffs(target, OnNPCHit);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            BuffChance.ApplyBuffs(target, OnNPCHit);
        }
    }
}
