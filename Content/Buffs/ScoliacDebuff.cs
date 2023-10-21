using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Buffs;

public class ScoliacDebuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
        Main.debuff[Type] = true;
    }
    public override string Texture => "Terraria/Images/Buff_" + BuffID.ThornWhipNPCDebuff;
}
internal class ScoliacDebuffGlobalNPC : GlobalNPC
{
    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        if (npc.HasBuff<ScoliacDebuff>() && projectile.DamageType == DamageClass.Summon)
        {
            modifiers.FlatBonusDamage += Scoliac.TAG_DAMAGE;
            if (Main.rand.Next(0, 100) < Scoliac.TAG_POISON_CHANCE)
                npc.AddBuff(BuffID.Poisoned, 600);
            if (Main.rand.Next(0, 100) < Scoliac.TAG_VENOM_CHANCE)
                npc.AddBuff(BuffID.Venom, 420);
        }
    }
}