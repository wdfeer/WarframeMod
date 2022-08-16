using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarframeMod.Vanilla
{
    internal class VanillaWeapon : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.ModItem != null)
                return;
            (int, int) extraDmgAndCrit = GetExtraDamageAndCrit(item);
            item.damage += extraDmgAndCrit.Item1;
            item.crit += extraDmgAndCrit.Item2;
        }
        (int,int) GetExtraDamageAndCrit(Item item)
        {
            if (item.DamageType == DamageClass.MeleeNoSpeed)
                return ((int)(item.damage * -0.06f), 7);
            else if (item.DamageType == DamageClass.Melee)
                return ((int)(item.damage * -0.07f), 9);
            else if (item.DamageType == DamageClass.Ranged)
                return ((int)(item.damage * -0.05f), 5);
            else if (item.DamageType == DamageClass.Magic)
                return ((int)(item.damage * -0.06f), 6);
            return (0, 0);
        }
    }
}
