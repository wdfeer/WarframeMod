using Terraria.DataStructures;

namespace WarframeMod.Common.Players;

internal class DamageResistancePlayer : ModPlayer
{
    public List<float> resists = new List<float>();
    public float DamageMultiplier => resists.Any() ? resists.Select(x => 1 - x).Aggregate((x,y) => x * y) : 1f;
    public override void ResetEffects()
    {
        resists = new List<float>();
    }
    public void AddDR(float resist)
    {
        resists.Add(resist);
    }
    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        damage = (int)(damage * DamageMultiplier);
        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    }
}
