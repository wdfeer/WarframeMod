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

        SpawnDusts(player, oldLength, newLength);
    }
    void ModifyHitboxSize(Player player, ref Rectangle hitbox, float sizeMult, int sizeIncrease, out float oldLength, out float newLength)
    {
        Vector2 GetFurthestCorner(Rectangle rect, Vector2 origin)
        {
            Vector2[] corners = new Vector2[]
            {
                rect.TopLeft(), rect.TopRight(),rect.BottomLeft(), rect.BottomRight()
            };
            return corners.MaxBy(v2 => v2.Distance(origin));
        }

        Vector2 oldFurthest = GetFurthestCorner(hitbox, player.Center);
        Vector2 relativeFurthest = oldFurthest - player.Center;
        oldLength = relativeFurthest.Length();
        relativeFurthest *= sizeMult;
        relativeFurthest += Vector2.Normalize(relativeFurthest) * sizeIncrease;
        newLength = relativeFurthest.Length();
        hitbox = new Rectangle((int)player.Center.X, (int)player.Center.Y, (int)relativeFurthest.X, (int)relativeFurthest.Y);
        FixNegativeRectangleDimensions(ref hitbox);
    }
    void FixNegativeRectangleDimensions(ref Rectangle rect)
    {
        Point newPos = rect.Location;
        Point newSize = rect.Size().ToPoint();
        if (rect.Width < 0)
        {
            newPos.X += rect.Width;
            newSize.X = -newSize.X;
        }
        if (rect.Height < 0)
        {
            newPos.Y += rect.Height;
            newSize.Y = -newSize.Y;
        }
        rect = new Rectangle(newPos.X, newPos.Y, newSize.X, newSize.Y);
    }
    void SpawnDusts(Player player, float innerRadius, float outerRadius)
    {
        Vector2 direction = Vector2.UnitX.RotatedBy(player.itemRotation) * player.direction;
        float mult = 0.8f;
        Vector2 start = player.Center + direction * innerRadius * mult;
        Vector2 end = player.Center + direction * outerRadius * mult;
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
