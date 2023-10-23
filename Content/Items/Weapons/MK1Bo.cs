using Terraria.Localization;

namespace WarframeMod.Content.Items.Weapons;

internal class MK1Bo : ModItem
{
    public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DEFENSE_PENETRATION);
    public const int DEFENSE_PENETRATION = 6;
    public override string Texture => "WarframeMod/Content/Items/Weapons/Bo";
    public override void SetDefaults()
    {
        Item.ArmorPenetration = 6;
        Item.damage = 11;
        Item.crit = 6;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.width = 57;
        Item.height = 60;
        Item.scale = 1.75f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.useTime = 42;
        Item.useAnimation = 42;
        Item.autoReuse = true;
        Item.rare = 1;
        Item.value = Item.buyPrice(silver: 25);
    }
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        float rot = player.itemRotation + MathHelper.PiOver4;
        if (player.direction == 1)
            rot += MathHelper.PiOver2;
        Vector2 rotationDirection = rot.ToRotationVector2();
        player.itemLocation += rotationDirection * player.itemWidth * 1.4f;
    }
    public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        int radius = (int)(player.itemWidth * 1.4f);
        Point pos = (player.Center - new Vector2(radius, radius)).ToPoint();
        hitbox = new Rectangle(pos.X, pos.Y, radius * 2, radius * 2);
        hitbox.X += player.direction;
    }
}