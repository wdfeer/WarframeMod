namespace WarframeMod.Common;
public class BleedingBuff
{
    [Obsolete]
    public static void Create(float hitDamage, NPC target, bool netSync = true)
        => DotBuff.Create(StackableBuff.Bleeding, (int)hitDamage, target, netSync);
}
