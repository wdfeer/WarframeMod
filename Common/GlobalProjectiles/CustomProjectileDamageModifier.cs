﻿// namespace WarframeMod.Common.GlobalProjectiles;

// internal class CustomProjectileDamageModifier : GlobalProjectile
// {
//     public override bool InstancePerEntity => true;
//     public Func<Projectile, int, Entity, int> modifyDamage = (proj, oldDamage, target) => oldDamage;
//     void ModifyDamage(Projectile projectile, Entity target, ref int damage, ref bool crit)
//     {
//         damage = modifyDamage(projectile, damage, target);
//     }
//     public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
//         => ModifyDamage(projectile, target, ref damage, ref crit);
//     public override void ModifyHitPlayer(Projectile projectile, Player target, ref int damage, ref bool crit)
//         => ModifyDamage(projectile, target, ref damage, ref crit);
//     public override void ModifyHitPvp(Projectile projectile, Player target, ref int damage, ref bool crit)
//         => ModifyDamage(projectile, target, ref damage, ref crit);
// }
