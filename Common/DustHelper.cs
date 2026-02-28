namespace WarframeMod.Common;

public static class DustHelper
{
    public static Dust[] RectangleCorners(Rectangle rect, short id, float velocityMult = 1f, bool gravity = true)
    {
        Dust[] dusts = new Dust[4];

        Vector2[] corners = [
            rect.TopLeft(), rect.TopRight(),rect.BottomLeft(), rect.BottomRight()
        ];

        for (int i = 0; i < corners.Length; i++)
        {
            dusts[i] = Dust.NewDustPerfect(corners[i], id);
            dusts[i].velocity *= velocityMult;
            dusts[i].noGravity = !gravity;
        }

        return dusts;
    }

            public static void NewDustsCircleEdge(int count, Vector2 center, float radius, int type, Action<Dust> edit = null)
        {
            NewDustsCustom(count,
             () => Dust.NewDustPerfect(center + Main.rand.NextVector2CircularEdge(radius, radius), type),
             edit);
        }
        /// <summary>
        /// Spawns dusts in a circle and applies a velocity from the center of the circle to them
        /// </summary>
        public static void NewDustsCircleFromCenter(int count, Vector2 center, float radius, int type, float speed, Action<Dust> edit = null)
        {
            NewDustsCircle(count, center, radius, type, (dust) =>
            {
                dust.velocity += Vector2.Normalize(dust.position - center) * speed;
                if (edit != null)
                    edit(dust);
            });
        }
        public static void NewDustsCircle(int count, Vector2 center, float radius, int type, Action<Dust> edit = null)
        {
            NewDustsCustom(count,
             () => Dust.NewDustPerfect(center + Main.rand.NextVector2Circular(radius, radius), type),
             edit);
        }
        public static void NewDustsPerfect(int count, Vector2 position, int type, Action<Dust> edit = null)
        {
            NewDustsCustom(count,
             () => Dust.NewDustPerfect(position, type),
             edit);
        }
        /// <summary>
        /// Spawns dusts with the create function, calls the edit on each of them (if edit is specified)
        /// </summary>
        /// <param name="count">Number of dusts to spawn</param>
        /// <param name="create">Function that returns a dust</param>
        public static void NewDustsCustom(int count, Func<Dust> create, Action<Dust> edit = null)
        {
            for (int i = 0; i < count; i++)
            {
                Dust dust = create();
                if (edit != null)
                    edit(dust);
            }
        }
}