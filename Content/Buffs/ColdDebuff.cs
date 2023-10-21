namespace WarframeMod.Content.Buffs;

public class ColdDebuff : ModBuff
{
    public override string Texture => "Terraria/Images/Buff_" + BuffID.Slow;
}
internal class ColdDebuffGlobalNPC : GlobalNPC
{
    public override void AI(NPC npc)
    {
        if (!npc.HasBuff<ColdDebuff>())
            return;
        if (Main.rand.NextBool(WarframeMod.IsBossAlive() ? 4 : 1))
        {
            npc.velocity.X *= 0.95f;
            npc.velocity.Y *= 0.975f;
        }

        Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Frost);
        d.velocity *= 0.9f;
        d.noGravity = true;
    }
}