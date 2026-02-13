using WarframeMod.Common;
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
            modifiers.FlatBonusDamage += Scoliac.TAG_DAMAGE;
    }

    public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
        if (npc.HasBuff<ScoliacDebuff>() && projectile.DamageType == DamageClass.Summon &&
            Main.rand.Next(0, 100) < Scoliac.TAG_TOXIN_CHANCE)
            DotBuff.Create(StackableBuff.Toxin, damageDone, npc);
    }
}