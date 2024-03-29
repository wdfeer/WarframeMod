﻿namespace WarframeMod.Content.Projectiles;

internal class MagnetizeProjectile : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DiamondBolt;
    public NPC Target
    {
        get => Main.npc[(int)Projectile.ai[0]];
        set
        {
            Projectile.ai[0] = value.whoAmI;
            Projectile.netUpdate = true;
        }
    }
    public override void SetDefaults()
    {
        Projectile.hide = true;
        Projectile.width = 240;
        Projectile.timeLeft = 600;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
    }
    public override void AI()
    {
        if (Target == null || !Target.active)
        {
            Projectile.timeLeft = 0;
            return;
        }
        Projectile.Center = Target.Center;

        Projectile[] projectiles = GetValidIntersectingProjectiles();
        foreach (var proj in projectiles)
        {
            proj.velocity += Vector2.Normalize(Projectile.Center - proj.Center);
        }

        SpawnDusts();
    }
    public override bool ShouldUpdatePosition() => false;
    Projectile[] GetValidIntersectingProjectiles()
    {
        return Main.projectile.Where(x =>
        {
            return x.active
                   && x.friendly
                   && !Main.projPet[x.type]
                   && x.Center.Distance(Projectile.Center) < Projectile.width;
        }).ToArray();
    }
    void SpawnDusts()
    {
        for (int i = 0; i < Projectile.width / 8; i++)
        {
            Vector2 position = Projectile.Center + Main.rand.NextVector2CircularEdge(Projectile.width, Projectile.width);
            Dust d = Dust.NewDustDirect(position, 1, 1, DustID.MagnetSphere);
            d.noGravity = true;
        }
    }
}