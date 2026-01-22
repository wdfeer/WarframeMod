namespace WarframeMod.Content.Items.Weapons;

public abstract class CircularMelee : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
    public virtual float SizeMult => 1.5f;
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        float rotation = player.itemRotation + MathHelper.PiOver4;
        if (player.direction == 1)
            rotation += MathHelper.PiOver2;
        Vector2 rotationDirection = rotation.ToRotationVector2();
        player.itemLocation += rotationDirection * player.itemWidth * SizeMult;
    }
    public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        int radius = (int)(player.itemWidth * SizeMult);
        Point pos = (player.Center - new Vector2(radius, radius)).ToPoint();
        hitbox = new Rectangle(pos.X, pos.Y, radius * 2, radius * 2);
        hitbox.X += player.direction;
    }
}