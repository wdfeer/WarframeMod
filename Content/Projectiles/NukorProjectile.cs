using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using Terraria.GameContent;

namespace WarframeMod.Content.Projectiles;

public class NukorProjectile : ModProjectile
{
    // The maximum charge value
    private const float MAX_CHARGE = 40f;
    //The distance charge particle from the player center
    private const float MOVE_DISTANCE = 60f;
    public float Distance
    {
        get => Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    // The actual charge value is stored in the localAI0 field
    public float Charge
    {
        get => Projectile.localAI[0];
        set => Projectile.localAI[0] = value;
    }

    public bool IsAtMaxCharge => Charge == MAX_CHARGE;

    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.hide = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 7;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Projectile.type);
        Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
        // We start drawing the laser if we have charged up
        if (IsAtMaxCharge)
        {
            DrawLaser(texture, Main.player[Projectile.owner].Center,
                Projectile.velocity, 10, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
        }
        return false;
    }
    public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default, int transDist = 50)
    {
        float r = unit.ToRotation() + rotation;

        // Draws the laser 'body'
        for (float i = transDist; i <= Distance; i += step)
        {
            Color c = Color.White;
            var origin = start + i * unit;
            Main.EntitySpriteDraw(texture, origin - Main.screenPosition,
                new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
                new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
        }

        // Draws the laser 'tail'
        Main.EntitySpriteDraw(texture, start + unit * (transDist - step) - Main.screenPosition,
            new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

        // Draws the laser 'head'
        Main.EntitySpriteDraw(texture, start + (Distance + step) * unit - Main.screenPosition,
            new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
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
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
            player.Center + unit * Distance, 22, ref point);
    }

    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (crit)
            damage *= 2;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.position = player.Center + Projectile.velocity * MOVE_DISTANCE;
        Projectile.timeLeft = 2;

        UpdatePlayer(player);
        ChargeLaser(player);

        if (Charge < MAX_CHARGE) return;

        SetLaserPosition(player);
        SpawnDusts(player);
        CastLights();
    }

    private void SpawnDusts(Player player)
    {
        Vector2 dustPos = player.Center + Projectile.velocity * Distance;

        for (int i = 0; i < 2; ++i)
        {
            float num1 = Projectile.velocity.ToRotation() + (Main.rand.NextBool() ? -1.0f : 1.0f) * 1.57f;
            float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
            Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
            Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.OrangeTorch, dustVel.X, dustVel.Y)];
            dust.noGravity = true;
            dust.scale = 1.2f;
        }

        if (Main.rand.NextBool(5))
        {
            Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
            Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
            dust.velocity *= 0.5f;
            dust.velocity.Y = -Math.Abs(dust.velocity.Y);
            Vector2 unit = dustPos - Main.player[Projectile.owner].Center;
            unit.Normalize();
            dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + 55 * unit, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
            dust.velocity = dust.velocity * 0.5f;
            dust.velocity.Y = -Math.Abs(dust.velocity.Y);
        }
    }

    /*
		* Sets the end of the laser position based on where it collides with something
		*/
    private void SetLaserPosition(Player player)
    {
        for (Distance = MOVE_DISTANCE; Distance <= 1200f; Distance += 5f)
        {
            var start = player.Center + Projectile.velocity * Distance;
            bool tileCollision = !Collision.CanHit(player.Center, 1, 1, start, 1, 1);
            bool npcCollision = Main.npc.Any(npc => npc.active && !npc.friendly && npc.getRect().Contains(start.ToPoint()));
            if (tileCollision)
            {
                Distance -= 5f;
                break;
            }
            if (npcCollision)
            {
                Distance += 5f;
                break;
            }
        }
    }

    private void ChargeLaser(Player player)
    {
        // Kill the Projectile if the player stops channeling
        if (!player.channel)
        {
            Projectile.Kill();
        }
        else
        {
            // Do we still have enough mana? If not, we kill the Projectile because we cannot use it anymore
            if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
            {
                Projectile.Kill();
            }
            Vector2 offset = Projectile.velocity;
            offset *= MOVE_DISTANCE - 20;
            Vector2 pos = player.Center + offset - new Vector2(10, 10);
            if (Charge < MAX_CHARGE)
            {
                Charge++;
            }
            int chargeFact = (int)(Charge / 20f);
            Vector2 dustVelocity = Vector2.UnitX * 18f;
            dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
            Vector2 spawnPos = Projectile.Center + dustVelocity;
            for (int k = 0; k < chargeFact + 1; k++)
            {
                Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
                Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, DustID.AncientLight, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f)];
                dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
                dust.noGravity = true;
                dust.scale = Main.rand.Next(10, 20) * 0.05f;
                dust.color = Color.Orange;
            }
        }
    }

    private void UpdatePlayer(Player player)
    {
        // Multiplayer support here, only run this code if the client running it is the owner of the Projectile
        if (Projectile.owner == Main.myPlayer)
        {
            Vector2 diff = Main.MouseWorld - player.Center;
            diff.Normalize();
            Projectile.velocity = diff;
            Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
            Projectile.netUpdate = true;
        }
        int dir = Projectile.direction;
        player.ChangeDir(dir); // Set player direction to where we are shooting
        player.heldProj = Projectile.whoAmI; // Update player's held Projectile
        player.itemTime = 2; // Set item time to 2 frames while we are used
        player.itemAnimation = 2; // Set item animation time to 2 frames while we are used
        player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir); // Set the item rotation to where we are shooting
    }

    private void CastLights()
    {
        // Cast a light along the line of the laser
        DelegateMethods.v3_1 = new Vector3(1f, 1f, 0.8f);
        Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
    }

    public override bool ShouldUpdatePosition() => false;

    /*
		* Update CutTiles so the laser will cut tiles (like grass)
		*/
    public override void CutTiles()
    {
        DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
        Vector2 unit = Projectile.velocity;
        Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
    }
}
