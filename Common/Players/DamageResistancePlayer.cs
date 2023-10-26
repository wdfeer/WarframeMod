using Terraria.DataStructures;
using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Common.Players;

internal class DamageResistancePlayer : ModPlayer
{
    public List<float> resists = new List<float>();
    public float ResistsMult => resists.Any() ? resists.Select(x => 1 - x).Aggregate((x, y) => x * y) : 1f;
    public List<float> multipliers = new List<float>();
    public float MultsMult => multipliers.Any() ? multipliers.Aggregate((x, y) => x * y) : 1f;
    public float TotalDamageMultiplier => ResistsMult * MultsMult * EnemyBuff.GetDamageMult();
    public override void ResetEffects()
    {
        resists = new List<float>();
        multipliers = new List<float>();
    }
    public void AddDR(float resist)
    {
        resists.Add(resist);
    }
    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
        modifiers.SourceDamage *= TotalDamageMultiplier;
    }
}
