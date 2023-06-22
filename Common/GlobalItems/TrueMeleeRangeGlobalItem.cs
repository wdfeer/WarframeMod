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

        // DustHelper.RectangleCorners(hitbox, DustID.GemRuby, 0, false);
        ModifyHitboxSize(player, ref hitbox, modPl.rangeMult, modPl.absoluteExtraRange, out float oldLength, out float newLength, out bool radialAttack);
        // DustHelper.RectangleCorners(hitbox, DustID.GemSapphire, 0, false);

        SpawnDusts(player, oldLength, newLength);
        if (radialAttack)
            SpawnDusts(player, oldLength, newLength, MathF.PI);
    }
    void ModifyHitboxSize(Player player, ref Rectangle hitbox, float sizeMult, int sizeIncrease, out float oldLength, out float newLength, out bool radialAttack)
    {
        bool IsRadialAttack(Player player, Rectangle hitbox)
        {
            IEnumerable<float> distances = GetRectCorners(hitbox).Select(x => player.Center.Distance(x));
            return distances.Max() / distances.Min() < 1.05f;
        }
        Vector2 GetFurthestCorner(Rectangle rect, Vector2 origin)
        {
            Vector2[] corners = GetRectCorners(rect);
            return corners.MaxBy(v2 => v2.Distance(origin));
        }

        oldLength = GetFurthestCorner(hitbox, player.Center).Distance(player.Center);
        radialAttack = IsRadialAttack(player, hitbox);

        if (radialAttack)
        {
            sizeMult = 1 + (sizeMult - 1) * 2;
            sizeIncrease *= 2;

            Point size = new Point((int)(hitbox.Width * sizeMult + sizeIncrease),
                (int)(hitbox.Height * sizeMult + sizeIncrease));
            hitbox = new Rectangle(
                (int)(player.Center.X - size.X / 2f),
                (int)(player.Center.Y - size.Y / 2f),
                size.X,
                size.Y
            );
        }
        else
        {
            var relativeFurthest = GetFurthestCorner(hitbox, player.Center) - player.Center;
            relativeFurthest *= sizeMult;
            relativeFurthest += Vector2.Normalize(relativeFurthest) * sizeIncrease;
            hitbox = new Rectangle((int)player.Center.X, (int)player.Center.Y, (int)relativeFurthest.X, (int)relativeFurthest.Y);
        }
        newLength = GetFurthestCorner(hitbox, player.Center).Distance(player.Center);

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
    void SpawnDusts(Player player, float innerRadius, float outerRadius, float rotationOffset = 0f)
    {
        Vector2 direction = Vector2.UnitX.RotatedBy(player.itemRotation) * player.direction;
        direction = direction.RotatedBy(rotationOffset);

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

    Vector2[] GetRectCorners(Rectangle rect) =>
            new Vector2[]
            {
                rect.TopLeft(), rect.TopRight(),rect.BottomLeft(), rect.BottomRight()
            };
}
