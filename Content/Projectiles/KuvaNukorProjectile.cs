using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using WarframeMod.Content.Items;

namespace WarframeMod.Content.Projectiles;

public class KuvaNukorProjectile : BeamProjectile
{
    public override string Texture => "WarframeMod/Content/Projectiles/NukorProjectile";
    protected override float MaxCharge => 30f;
    protected override float MinDistance => 60f;
    private const int maxChildLasers = 3;
    public Vector2[] childLaserDestinations = new Vector2[maxChildLasers];
    public override DamageClass DamageClass => DamageClass.Magic;
    public override int HitCooldown => 6;
    protected override int WeaponEnergyDustType => DustID.OrangeTorch;
    protected override int Contact1DustType => DustID.Smoke;
    protected override int Contact2DustType => DustID.Smoke;
    public override SoundStyle? ChargedSound => SoundID.DD2_BetsyWindAttack.ModifySoundStyle(volume: 0.2f, pitchVariance: 0.12f);
    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Projectile.type);
        Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
        if (IsAtMaxCharge)
        {
            DrawLaser(texture, Main.player[Projectile.owner].Center,
                Projectile.velocity, 10, -1.57f, 1f, Distance, Color.White, (int)MinDistance);
            for (int i = 0; i < childLaserDestinations.Length; i++)
            {
                Vector2 childEnd = childLaserDestinations[i];
                Vector2 beamEnd = BeamEnd;
                Vector2 childVector = Vector2.Normalize(childEnd - beamEnd);
                float childLength = (childEnd - beamEnd).Length();
                DrawLaser(
                    texture,
                    beamEnd,
                    childVector * Projectile.velocity.Length(),
                    10,
                    -1.57f,
                    1f,
                    childLength,
                    Color.White,
                    (int)(MinDistance * 0.64f));
            }
        }
        return false;
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        // We can only collide if we are at max charge, which is when the laser is actually fired
        if (!IsAtMaxCharge) return false;

        Player player = Main.player[Projectile.owner];
        Vector2 unit = Projectile.velocity;
        float point = 0f;
        // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
        // It will look for collisions on the given line using AABB
        bool mainCollision = Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
            BeamEnd, 22, ref point);
        IEnumerable<bool> childCollisions = childLaserDestinations.Select(end => Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size()
            , BeamEnd, end, 22, ref point));
        bool childCollision = childCollisions.Any(boolean => boolean);
        return mainCollision || childCollision;
    }
    protected override void SetLaserPosition(Player player)
    {
        bool Hostile(NPC npc) => npc.active && !npc.friendly;
        childLaserDestinations = new Vector2[0];
        for (Distance = MinDistance; Distance <= 1200f; Distance += 5f)
        {
            Vector2 start = BeamEnd;
            bool tileCollision = !Collision.CanHit(player.Center, 1, 1, start, 1, 1);
            NPC hitNPC = Array.Find(Main.npc,
                        npc => Hostile(npc) && npc.getRect().Contains(start.ToPoint()));
            if (tileCollision)
            {
                Distance -= 5f;
                break;
            }
            if (hitNPC != null)
            {
                NPC[] nearbyNPCs = Array.FindAll(Main.npc,
                                  npc => Hostile(npc)
                                  && npc.whoAmI != hitNPC.whoAmI
                                  && npc.Center.Distance(start) < 128
                                  && Collision.CanHitLine(BeamEnd, 22, 1, npc.position, npc.width, npc.height));
                if (nearbyNPCs.Length > 0)
                {
                    childLaserDestinations = nearbyNPCs.Take(maxChildLasers)
                                     .Select(npc => npc.Center)
                                     .ToArray();
                }
                break;
            }
        }
    }
}
