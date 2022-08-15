using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarframeMod.Global;

namespace WarframeMod.Players
{
    internal class CritsPlayer : ModPlayer
    {
        public float critMultiplierPlayer = 1f;
        public float relativeCritChance = 0f;
        public override void ResetEffects()
        {
            critMultiplierPlayer = 1f;
            relativeCritChance = 0f;
        }
        public override void PostUpdateEquips()
        {
            if (Player.HeldItem.damage > 0)
                Player.GetCritChance(DamageClass.Generic) += (Player.HeldItem.crit + 4) * relativeCritChance;
        }
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
                float mult = critMultiplierPlayer;
                damage = (int)(damage * mult * critLevel);
                OverCritVisuals(target, knockback, critLevel);
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            var critLevel = GetCritLevel(proj.CritChance);
            if (crit && critLevel > 0)
            {
                float mult = critMultiplierPlayer * proj.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier;
                damage = (int)(damage * mult * critLevel);
                OverCritVisuals(target, knockback, critLevel);
            }
        }
        void OverCritVisuals(NPC target, float knockback, int critlvl)
        {
            float intensity = ModContent.GetInstance<WarframeClientConfig>().OvercritVisualIntensity;
            if (intensity <= 0.05f)
                return;
            Color dustColor = critlvl > 2 ? Color.Red : Color.Orange;
            for (int i = 0; i < 1 + knockback * intensity / 2; i++)
            {
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.PortalBoltTrail);
                dust.scale = knockback * (critlvl / 2) / 6 * (intensity < 1 ? intensity : 1) + 0.16f;
                dust.color = dustColor;
            }
        }
    }
}
