using WarframeMod.Content.Projectiles.Hostile;

namespace WarframeMod.Common.GlobalNPCs.Eximus;

public class ShockEximus : EximusVariant
{
    private const int ProjSpawnInterval = 8 * 60;
    private int projSpawnTimer;
    public static float OrbDamage => (Main.hardMode ? 80 : 30) * (Main.expertMode ? 2f : 1f);
    public override void AI(NPC npc)
    {
        if (enabled)
        {
            projSpawnTimer++;
            if (projSpawnTimer >= ProjSpawnInterval)
            {
                projSpawnTimer = 0;

                Projectile.NewProjectileDirect(npc.GetSource_FromThis(), npc.Center, Vector2.Zero,
                    ModContent.ProjectileType<ShockEximusProjectile>(), (int)OrbDamage, 0f);
                DustHelper.NewDustsCircleFromCenter(10, npc.Center, npc.width, DustID.Electric, 64f);
            }
        }
    }
}