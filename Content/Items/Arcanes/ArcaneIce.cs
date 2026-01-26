namespace WarframeMod.Content.Items.Arcanes;

public class ArcaneIce : Arcane
{
    public override void UpdateArcane(Player player)
    {
        player.buffImmune[BuffID.OnFire] = true;
        player.buffImmune[BuffID.OnFire3] = true;
        player.buffImmune[BuffID.Burning] = true;
        player.buffImmune[BuffID.Frostburn] = true;
        player.buffImmune[BuffID.Frostburn2] = true;
        player.buffImmune[BuffID.ShadowFlame] = true;
        player.buffImmune[BuffID.CursedInferno] = true;
        player.AddBuff(BuffID.Campfire, 1);
    }
}