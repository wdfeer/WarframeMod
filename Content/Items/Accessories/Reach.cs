namespace WarframeMod.Content.Items.Accessories;

public class Reach : ModItem
{
    public const int ABSOLUTE_RANGE_BONUS = 64;
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault($"+{ABSOLUTE_RANGE_BONUS / 15} tiles of True Melee range");
    }
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.width = 32;
        Item.height = 32;
        Item.rare = 2;
        Item.value = Item.buyPrice(silver: 66);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<TrueMeleeRangePlayer>().absoluteExtraRange += ABSOLUTE_RANGE_BONUS;
    }
}
class TrueMeleeRangePlayer : ModPlayer
{
    public int absoluteExtraRange;
    public float rangeMult;
    public override void ResetEffects()
    {
        absoluteExtraRange = 0;
        rangeMult = 1f;
    }
}
class TrueMeleeRangeGlobalItem : GlobalItem
{
    public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        if (item.noMelee || (item.DamageType != DamageClass.Melee && item.DamageType != DamageClass.MeleeNoSpeed))
            return;
        var modPl = player.GetModPlayer<TrueMeleeRangePlayer>();
        if (modPl.rangeMult == 1f && modPl.absoluteExtraRange == 0)
            return;

        ModifyHitboxSize(player, ref hitbox, modPl.rangeMult, modPl.absoluteExtraRange, out float oldLength, out float newLength);
        Vector2 direction = Vector2.UnitX.RotatedBy(player.itemRotation) * player.direction;
        float mult = 0.8f;
        Vector2 dustStart = player.Center + direction * oldLength * mult;
        Vector2 dustEnd = player.Center + direction * newLength * mult;
        SpawnDusts(dustStart, dustEnd);
    }
    void ModifyHitboxSize(Player player, ref Rectangle hitbox, float sizeMult, int sizeIncrease, out float oldLength, out float newLength)
    {
        Vector2 oldFurthest = new Vector2[]
        {
            hitbox.TopLeft(), hitbox.TopRight(),hitbox.BottomLeft(), hitbox.BottomRight()
        }.MaxBy(v2 => v2.Distance(player.Center));
        Vector2 relativeFurthest = oldFurthest - player.Center;
        oldLength = relativeFurthest.Length();
        relativeFurthest *= sizeMult;
        relativeFurthest += Vector2.Normalize(relativeFurthest) * sizeIncrease;
        newLength = relativeFurthest.Length();
        hitbox = new Rectangle((int)player.Center.X, (int)player.Center.Y, (int)relativeFurthest.X, (int)relativeFurthest.Y);
        FixRectangle(ref hitbox);
    }
    void FixRectangle(ref Rectangle rect)
    {
        Point newPos = rect.Location;
        Point newSize = rect.Size().ToPoint();
        if (rect.Width < 0)
        {
            newPos.X += rect.Width;
            newSize.X *= -1;
        }
        if (rect.Height < 0)
        {
            newPos.Y += rect.Height;
            newSize.Y *= -1;
        }
        rect = new Rectangle(newPos.X, newPos.Y, newSize.X, newSize.Y);
    }
    void SpawnDusts(Vector2 start, Vector2 end)
    {
        float length = end.Distance(start);
        float step = 15;
        Vector2 normal = Vector2.Normalize(end - start);
        for (float i = 0; i < length; (i, start) = (i + step, start + normal * step))
        {
            var dust = Dust.NewDustPerfect(start, DustID.TintableDustLighted, newColor: Color.White, Scale: 1.25f);
            dust.noGravity = true;
        }
    }
}