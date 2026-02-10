using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace WarframeMod.Content.Projectiles;

public class AstralTwilightProjectile : ModProjectile
{
    public const int PROJECTILE_LIFETIME = 5 * 60;

    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Melee;
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = PROJECTILE_LIFETIME;
        Projectile.light = 0.5f;
        Projectile.tileCollide = false;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.alpha = 0;
    }

    public override void AI()
    {
        if (Projectile.timeLeft < 30)
            Projectile.alpha = (int)(255f / 30f * Projectile.timeLeft);
        else if (Projectile.timeLeft > PROJECTILE_LIFETIME - 30)
            Projectile.alpha = (int)(255f / 30f * (PROJECTILE_LIFETIME - Projectile.timeLeft));
        else
        {
            if (Projectile.velocity.Length() < 32f)
                Projectile.velocity += Vector2.Normalize(Projectile.velocity) * 16f / 60f;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Projectile.timeLeft > 30)
            Projectile.timeLeft = 30;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Type);
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        Rectangle frame = texture.Frame();
        Vector2 origin = new Vector2(frame.Width / 2f, frame.Height / 2f);

        float scale = (float)Projectile.width / frame.Width;

        Vector2 pos = Projectile.Center - Main.screenPosition;

        Main.EntitySpriteDraw(texture,
            pos,
            frame,
            lightColor.MultiplyRGBA(new Color(Projectile.alpha, Projectile.alpha, Projectile.alpha, Projectile.alpha)),
            Projectile.rotation,
            origin,
            scale,
            SpriteEffects.None);

        return false;
    }
}