namespace WarframeMod.Common;

public static class DustHelper
{
    public static Dust[] RectangleCorners(Rectangle rect, short id, float velocityMult = 1f, bool gravity = true)
    {
        Dust[] dusts = new Dust[4];

        Vector2[] corners = new Vector2[]
        {
            rect.TopLeft(), rect.TopRight(),rect.BottomLeft(), rect.BottomRight()
        };

        for (int i = 0; i < corners.Length; i++)
        {
            dusts[i] = Dust.NewDustPerfect(corners[i], id);
            dusts[i].velocity *= velocityMult;
            dusts[i].noGravity = !gravity;
        }

        return dusts;
    }
}