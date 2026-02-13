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
        Player.UpdateTouchingTiles();
        bool touchingTiles = Player.TouchedTiles.Any();
        airborne = !touchingTiles;
    }
}