using WarframeMod.Common;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Buffs;

public class SecuraLectaDebuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
        Main.debuff[Type] = true;
    }

    public override string Texture => "Terraria/Images/Buff_" + BuffID.ThornWhipNPCDebuff;
}

internal class SecuraLectaDebuffGlobalNPC : GlobalNPC
{
    public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
        if (npc.HasBuff<SecuraLectaDebuff>() && hit.DamageType == DamageClass.Summon)
        {
            if (Main.rand.Next(0, 100) < SecuraLecta.TAG_ELECTRO_CHANCE)
            {
                ElectricityBuff.Create(damageDone, npc);
            }
        }
    }
}