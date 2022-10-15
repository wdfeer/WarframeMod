using Terraria.DataStructures;
using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Common.Players;

internal class DamageResistancePlayer : ModPlayer
{
    public List<float> resists = new List<float>();
    public float ResistsMult => resists.Any() ? resists.Select(x => 1 - x).Aggregate((x,y) => x * y) : 1f;
    public List<float> multipliers = new List<float>();
    public float MultsMult => multipliers.Any() ? multipliers.Aggregate((x,y) => x * y) : 1f;
    public float TotalDamageMultiplier => ResistsMult * MultsMult * EnemyBuff.DAMAGE_MULT;
    public override void ResetEffects()
    {
        resists = new List<float>();
        multipliers = new List<float>();
    }
    public void AddDR(float resist)
    {
        resists.Add(resist);
    }
    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        damage = (int)(damage * TotalDamageMultiplier);
        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    }
}
