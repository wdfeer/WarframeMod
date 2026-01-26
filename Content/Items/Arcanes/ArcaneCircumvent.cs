namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneCircumvent : Arcane
{
    public override void UpdateArcane(Player player)
    {
        player.GetModPlayer<ArcaneCircumventPlayer>().enabled = true;
    }
}

class ArcaneCircumventPlayer : ModPlayer
{
    public bool enabled;

    // These indicate what direction is what in the timer arrays used
    public const int DASH_RIGHT = 2;

    public const int DASH_LEFT = 3;

    // Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
    public const int DASH_COOLDOWN = 50;

    public const int DASH_DURATION = 35; // Duration of the dash afterimage effect in frames

    // The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
    public const float DASH_VELOCITY = 10f;

    // The direction the player has double tapped.  Defaults to -1 for no dash double tap
    public int dashDir = -1;

    public int dashDelay = 0; // frames remaining till we can dash again
    public int dashTimer = 0; // frames remaining in the dash

    public override void ResetEffects()
    {
        // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
        enabled = false;

        // ResetEffects is called not long after player.doubleTapCardinalTimer's values have been set
        // When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
        // If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
        if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DASH_RIGHT] < 15 &&
                 Player.doubleTapCardinalTimer[DASH_LEFT] == 0)
        {
            dashDir = DASH_RIGHT;
        }
        else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DASH_LEFT] < 15 &&
                 Player.doubleTapCardinalTimer[DASH_RIGHT] == 0)
        {
            dashDir = DASH_LEFT;
        }
        else
        {
            dashDir = -1;
        }
    }

    // This is the perfect place to apply dash movement, it's after the vanilla movement code, and before the player's position is modified based on velocity.
    // If they double tapped this frame, they'll move fast this frame
    public override void PreUpdateMovement()
    {
        // if the player can use our dash, has double tapped in a direction, and our dash isn't currently on cooldown
        if (CanUseDash() && dashDir != -1 && dashDelay == 0)
        {
            Vector2 newVelocity = Player.velocity;

            switch (dashDir)
            {
                // Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
                case DASH_LEFT when Player.velocity.X > -DASH_VELOCITY:
                case DASH_RIGHT when Player.velocity.X < DASH_VELOCITY:
                {
                    // X-velocity is set here
                    float dashDirection = dashDir == DASH_RIGHT ? 1 : -1;
                    newVelocity.X = dashDirection * DASH_VELOCITY;
                    break;
                }
                default:
                    return; // not moving fast enough, so don't start our dash
            }

            // start our dash
            dashDelay = DASH_COOLDOWN;
            dashTimer = DASH_DURATION;
            Player.velocity = newVelocity;

            // Here you'd be able to set an effect that happens when the dash first activates
            // Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
        }

        if (dashDelay > 0)
            dashDelay--;

        if (dashTimer > 0)
        {
            // dash is active
            // This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
            // Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
            // Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
            Player.eocDash = dashTimer;
            Player.armorEffectDrawShadowEOCShield = true;

            // count down frames remaining
            dashTimer--;
        }
    }

    private bool CanUseDash()
    {
        return enabled
               && Player.dashType ==
               DashID.None // player doesn't have Tabi or EoCShield equipped (give priority to those dashes)
               && !Player.setSolar // player isn't wearing solar armor
               && !Player.mount.Active; // player isn't mounted, since dashes on a mount look weird
    }
}