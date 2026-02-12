using WarframeMod.Common.GlobalNPCs;

namespace WarframeMod.Common;

public class DotBuff(StackableBuff type, float dps, int time = 300)
{
    public StackableBuff type = type;
    public float dps = dps;
    public int timeLeft = time;

    public static void Create(StackableBuff type, int hitDamage, NPC target, bool netSync = true)
    {
        target.GetGlobalNPC<DotDebuffNpc>().dots.Add(new DotBuff(type, hitDamage / 5f));
        if (Main.netMode != NetmodeID.SinglePlayer && netSync)
            WarframeMod.instance.SendStackableDebuffPacket(target.whoAmI, type, (int)hitDamage);
    }
}