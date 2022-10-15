using WarframeMod.Common.Players;

namespace WarframeMod.Common.GlobalItems;

class TrueMeleeRangeGlobalItem : GlobalItem
{
    public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        if (item.noMelee || item.DamageType != DamageClass.Melee && item.DamageType != DamageClass.MeleeNoSpeed)
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
        Vector2[] oldCorners = new Vector2[]
        {
            hitbox.TopLeft(), hitbox.TopRight(),hitbox.BottomLeft(), hitbox.BottomRight()
        };
        Vector2 oldFurthest = oldCorners.MaxBy(v2 => v2.Distance(player.Center));
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
