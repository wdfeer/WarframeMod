using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using Terraria.GameContent;

namespace WarframeMod.Content.Projectiles;

public abstract class BeamProjectile : ModProjectile
{
    // The maximum charge value
    protected virtual float MaxCharge => 40f;
    //The distance charge particle from the player center
    protected virtual float MoveDistance => 60f;
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
    public Player Owner => Main.player[Projectile.owner];
    public Vector2 BeamEnd => Owner.Center + Projectile.velocity * Distance;
    public bool IsAtMaxCharge => Charge >= MaxCharge;
    public abstract int HitCooldown { get; }
    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.hide = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = HitCooldown;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Projectile.type);
        Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
        // We start drawing the laser if we have charged up
        if (IsAtMaxCharge)
        {
            DrawLaser(texture, Owner.Center,
                Projectile.velocity, 10, -1.57f, 1f, 1000f, Color.White, (int)MoveDistance);
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

        Player player = Owner;
        Vector2 unit = Projectile.velocity;
        float point = 0f;
        // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
        // It will look for collisions on the given line using AABB
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
            player.Center + unit * Distance, 22, ref point);
    }
    public override void AI()
    {
        Player player = Owner;
        Projectile.position = player.Center + Projectile.velocity * MoveDistance;
        Projectile.timeLeft = 2;

        UpdatePlayer(player);
        ChargeLaser(player);

        SpawnDusts();

        if (!IsAtMaxCharge) return;

        SetLaserPosition(player);
        CastLights();
    }
    protected virtual int WeaponSmokeDustType => DustID.Smoke;
    protected abstract int WeaponEnergyDustType { get; }
    protected virtual Color WeaponEnergyDustColor => Color.White;
    protected abstract int Contact1DustType { get; }
    protected abstract int Contact2DustType { get; }
    protected virtual void SpawnContactDusts()
    {
        Vector2 dustPos = BeamEnd;
        if (Contact1DustType > -1)
            for (int i = 0; i < 2; ++i)
            {
                float num1 = Projectile.velocity.ToRotation() + (Main.rand.NextBool() ? -1.0f : 1.0f) * 1.57f;
                float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
                Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
                Dust dust = Dust.NewDustDirect(dustPos, 0, 0, Contact1DustType, dustVel.X, dustVel.Y);
                dust.noGravity = true;
                dust.scale = 1.2f;
            }
        if (Contact2DustType > -1 && Main.rand.NextBool(5))
        {
            Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
            Dust dust = Dust.NewDustDirect(dustPos + offset - Vector2.One * 4f, 8, 8, Contact2DustType, 0.0f, 0.0f, 100, new Color(), 1.5f);
            dust.velocity *= 0.5f;
            dust.velocity.Y = -Math.Abs(dust.velocity.Y);
        }
    }
    protected virtual void SpawnChargingDusts()
    {
        if (WeaponEnergyDustType < 0)
            return;
        Vector2 offset = Projectile.velocity;
        offset *= MoveDistance - 20;
        Vector2 pos = Owner.Center + offset - new Vector2(10, 10);
        int chargeFact = (int)(Charge / 20f);
        Vector2 dustVelocity = Vector2.UnitX * 18f;
        dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
        Vector2 spawnPos = Projectile.Center + dustVelocity;
        for (int k = 0; k < chargeFact + 1; k++)
        {
            Vector2 spawn = spawnPos + (Main.rand.NextFloat() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
            Dust dust = Dust.NewDustDirect(pos, 20, 20, WeaponEnergyDustType, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f);
            dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
            dust.noGravity = true;
            dust.scale = Main.rand.Next(10, 20) * 0.05f;
            dust.color = WeaponEnergyDustColor;
        }
    }
    protected virtual void SpawnWeaponSmokeDusts()
    {
        if (WeaponSmokeDustType > -1 && Main.rand.NextBool(5))
        {
            Vector2 unit = Vector2.Normalize(Projectile.velocity);
            Dust dust = Dust.NewDustDirect(Owner.Center + 55 * unit, 8, 8, WeaponSmokeDustType, Alpha: 100, Scale: 1.5f);
            dust.velocity = dust.velocity * 0.5f;
            dust.velocity.Y = -Math.Abs(dust.velocity.Y);
        }
    }
    private void SpawnDusts()
    {
        SpawnChargingDusts();
        if (!IsAtMaxCharge)
            return;
        SpawnContactDusts();
        SpawnWeaponSmokeDusts();
    }
    /// <summary>
    /// Move the end of the laser in the shooting direction by this value upon detecting a tile collision
    /// </summary>
    protected virtual float ExtraDistanceOnTileCollision => -5f;
    /// <summary>
    /// Move the end of the laser  in the shooting direction by this value upon detecting an NPC collision
    /// </summary>
    protected virtual float ExtraDistanceOnLastNPCCollision => 5f;
    protected virtual float MaxDistance => 1200f;
    protected virtual int MaxNPCsHit => 1;
    protected virtual Action OnDetectHitNPC => () => { };
    /*
		* Sets the end of the laser position based on where it collides with something
		*/
    protected virtual void SetLaserPosition(Player player)
    {
        int hitNPCs = 0;
        for (Distance = MoveDistance; Distance <= MaxDistance; Distance += 5f)
        {
            var start = player.Center + Projectile.velocity * Distance;
            bool tileCollision = !Collision.CanHit(player.Center, 1, 1, start, 1, 1);
            bool npcCollision = Main.npc.Any(npc => npc.active && !npc.friendly && npc.getRect().Contains(start.ToPoint()));
            if (tileCollision)
            {
                Distance += ExtraDistanceOnTileCollision;
                break;
            }
            if (npcCollision)
            {
                hitNPCs++;
                OnDetectHitNPC();
                if (hitNPCs >= MaxNPCsHit)
                {
                    Distance += ExtraDistanceOnLastNPCCollision;
                    break;
                }
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
            if (Charge < MaxCharge)
            {
                Charge++;
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

    protected virtual void CastLights()
    {
        // Cast a light along the line of the laser
        DelegateMethods.v3_1 = new Vector3(1f, 1f, 0.8f);
        Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MoveDistance), 26, DelegateMethods.CastLight);
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
