using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;

namespace WarframeMod.Items
{
    public static class WeaponCommon
    {
        public static SoundStyle ModifySoundStyle(SoundStyle style, float volume = 1f, float pitchVariance = 0f)
        {
            style.Volume *= volume;
            style.PitchVariance = pitchVariance;
            return style;
        }
        public static void ModifyProjectileSpawnPosition(ref Vector2 position, Vector2 velocity, float offset)
        {
            Vector2 muzzleOffset = velocity.SafeNormalize(Vector2.Zero) * offset;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
        }
    }
}
