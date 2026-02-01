using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace WarframeMod.Content.Projectiles;

public class WolfSledgeProjectile : ExplosiveProjectile
{
    public override string Texture => "WarframeMod/Content/Items/Weapons/WolfSledge";
    public override int ExplosionWidth => 12 * 16;
    public override bool ExplodeOnNPCHit => false;
    public override bool ExplodeOnTileCollide => false;
    public override float ExplosionSoundVolume => 1f;

    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.width = 56;
        Projectile.height = 56;
        Projectile.scale = 2.5f;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 30;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        Projectile.rotation += MathF.PI * 5 / 60f;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Type);
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        Rectangle frame = new Rectangle(0, 0, 56, 56);
        Vector2 origin = new Vector2(frame.Width / 2f, frame.Height / 2f);

        Vector2 pos = Projectile.Center - Main.screenPosition;
        
        Main.EntitySpriteDraw(texture,
            pos,
            frame,
            lightColor,
            Projectile.rotation,
            origin,
            Projectile.scale,
            SpriteEffects.None);
        
        return false;
    }
}