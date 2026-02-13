
namespace WarframeMod.Common.Players;

public class AirbornePlayer : ModPlayer
{
    private bool airborne;
    private bool checkedThisFrame;

    public bool Airborne
    {
        get
        {
            if (!checkedThisFrame) CheckAirborne();
            return airborne;
        }
    }

    public override void ResetEffects()
    {
        checkedThisFrame = false;
    }

    private void CheckAirborne()
    {
        checkedThisFrame = true;
        
        List<Point> touchingTiles = new();
        Collision.GetEntityEdgeTiles(touchingTiles, Player);
        airborne = touchingTiles.All(pos => !Main.tile[pos].HasTile);
    }
}