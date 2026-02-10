using Terraria.Audio;
using WarframeMod.Content.Projectiles;

namespace WarframeMod.Content.Items.Accessories;

public class AstralTwilight : ModItem
{
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 44;
        Item.height = 64;
        Item.rare = 4;
        Item.value = Item.buyPrice(gold: 33);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AstralTwilightPlayer>().enabled = true;
        player.GetModPlayer<AstralTwilightPlayer>().timer--;
    }
}

public class AstralTwilightPlayer : ModPlayer
{
    public bool enabled;
    public int timer;

    public override void ResetEffects()
        => enabled = false;

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (enabled && (proj.DamageType.CountsAsClass(DamageClass.Melee) ||
                        proj.DamageType.CountsAsClass(DamageClass.Throwing)) && timer <= 0 && damageDone > 3)
        {
            Activate(damageDone / 3, proj.DamageType);
            timer = 60;
        }
    }

    void Activate(int damage, DamageClass damageClass)
    {
        var pos = Player.Center + (Vector2.One * 64f).RotateRandom(MathF.Tau);
        var direction = Main.MouseWorld.DirectionFrom(pos);
        Projectile proj = Projectile.NewProjectileDirect(Player.GetSource_FromThis(), pos, direction * 0.1f,
            ModContent.ProjectileType<AstralTwilightProjectile>(), damage, 0f, Player.whoAmI);
        proj.DamageType = damageClass;

        SoundEngine.PlaySound(SoundID.Item43, pos);
    }
}