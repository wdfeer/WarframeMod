namespace WarframeMod.Common;
public class ElectricityBuff
{
    [Obsolete]
    public static void Create(float hitDamage, NPC target, bool netSync = true)
        => DotBuff.Create(StackableBuff.Electricity, (int)hitDamage, target, netSync);
}
