using Terraria.Audio;
using WarframeMod.Common.GlobalProjectiles;
using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Projectiles;
public class NatarukProjectile : ModProjectile
{
    const float MOVE_DISTANCE = 40;
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RubyBolt;
    public override void SetDefaults()
    {
        Projectile.tileCollide = false;
        Projectile.scale = 0f;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 120;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.friendly = false;
    }
    public int CurrentCharge => 120 - Projectile.timeLeft;
    public const int PERFECT_CHARGE = 40;
    public bool Perfect => !fullyCharged && CurrentCharge >= PERFECT_CHARGE;
    public bool fullyCharged = false;
    public override bool PreAI()
    {
        if (Projectile.friendly)
        {
            Player player = Main.player[Projectile.owner];
            int dustId = GetLaunchedDustId();
            new Action[]
            {
                () => Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.OrangeTorch),
                () => Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.OrangeTorch, Scale: 1.25f),
                () =>
                {
                    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Red, Scale: 2f);
                    d.velocity *= 0.16f;
                    d.noGravity = true;
                    for (int i = 0; i < 2; i++)
                    {
                        Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Red).noGravity = true;
                    }
                },
                () =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch, Scale: 1.15f);
                    }
                }
            }
            [(int)launchedLevel]();
            return false;
        }
        return true;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (!player.channel)
        {
            if (Projectile.owner == Main.myPlayer)
                Launch();
        }
        else
        {
            if (fullyCharged)
            {
                Projectile.timeLeft = 60;
            }
            else if (CurrentCharge == PERFECT_CHARGE)
            {
                SoundEngine.PlaySound(SoundID.MaxMana.ModifySoundStyle(), Projectile.Center);
            }
            else if (CurrentCharge >= 60)
            {
                fullyCharged = true;
                SoundEngine.PlaySound(SoundID.MaxMana.WithPitchOffset(-0.4f), Projectile.Center);
            }

            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 diff = Main.MouseWorld - player.Center;
                diff.Normalize();
                Projectile.velocity = diff;
                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;
            }
            int dir = Projectile.direction;
            player.ChangeDir(dir);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 16;
            player.itemAnimation = 16;
            player.itemRotation = MathF.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir);
            Projectile.rotation = player.itemRotation + MathHelper.PiOver2 + MathHelper.Pi;
            Projectile.position = player.Center + Vector2.Normalize(Projectile.velocity) * MOVE_DISTANCE;
            if (Projectile.velocity.X > 0)
                Projectile.position -= Projectile.velocity * MOVE_DISTANCE;

            if ((fullyCharged || CurrentCharge > 40) && Main.rand.NextBool(2))
                Dust.NewDustPerfect(
                    player.Center + Vector2.Normalize(Projectile.velocity) * MOVE_DISTANCE * 0.45f,
                    DustID.RedTorch,
                    Scale: 0.6f);
        }
    }
    ChargeLevel launchedLevel = ChargeLevel.Quick;
    int GetLaunchedDustId() => new int[] { DustID.OrangeTorch, DustID.OrangeTorch, DustID.Firework_Red, DustID.RedTorch }[(int)(launchedLevel)];
    enum ChargeLevel
    {
        Quick = 0,
        Medium = 1,
        Perfect = 2,
        Full = 3
    }
    ChargeLevel GetChargeLevel()
    {
        if (fullyCharged)
            return ChargeLevel.Full;
        if (Perfect)
            return ChargeLevel.Perfect;
        if (CurrentCharge > 15)
            return ChargeLevel.Medium;
        return ChargeLevel.Quick;
    }
    void Launch()
    {
        launchedLevel = GetChargeLevel();
        int lvl = (int)launchedLevel;
        int size = new int[] { 12, 16, 32, 28 }[lvl];
        Projectile.Resize(size, size);
        Projectile.friendly = true;
        Projectile.extraUpdates = 9;
        Projectile.netUpdate = true;
        Projectile.velocity *= 16;
        Projectile.timeLeft = 180;
        Projectile.tileCollide = launchedLevel == ChargeLevel.Quick;
        SoundStyle[] sounds = new SoundStyle[] { SoundID.Item5, SoundID.Item5.WithPitchOffset(-0.4f), SoundID.Item43.WithPitchOffset(0.5f), SoundID.Item8 };
        SoundEngine.PlaySound(sounds[lvl], Projectile.Center);
        float[] damageMults = new float[] { 1f, 1.5f, 2f, 2f };
        Projectile.damage = (int)(Projectile.damage * damageMults[lvl]);
        Projectile.knockBack *= damageMults[lvl];
        int baseCritChance = new int[] { Nataruk.BASE_CRIT_UNCHARGED, Nataruk.BASE_CRIT_CHARGED - 15, Nataruk.BASE_CRIT_PERFECT, Nataruk.BASE_CRIT_CHARGED }[lvl];
        int modifiedBaseCritChance = (int)(baseCritChance * Main.LocalPlayer.GetModPlayer<CritPlayer>().BaseCritChanceMult);
        Projectile.CritChance = Projectile.CritChance - Nataruk.BASE_CRIT_UNCHARGED + modifiedBaseCritChance;
        Projectile.GetGlobalProjectile<CritGlobalProjectile>().CritMultiplier = new float[] { Nataruk.CRIT_MULT_UNCHARGED, Nataruk.CRIT_MULT_CHARGED - 0.1f, Nataruk.CRIT_MULT_PERFECT, Nataruk.CRIT_MULT_CHARGED }[lvl];
        Projectile.penetrate = new int[] { 3, 5, -1, -1 }[lvl];
    }
}