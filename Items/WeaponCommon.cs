using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace WarframeMod.Items
{
    public static class WeaponCommon
    {
        public static void ModifyProjectileSpawnPosition(ref Vector2 position, Vector2 velocity, float offset)
        {
            Vector2 muzzleOffset = velocity.SafeNormalize(Vector2.Zero) * offset;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
        }
    }
}
