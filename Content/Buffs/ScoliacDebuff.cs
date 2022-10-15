using WarframeMod.Common.GlobalNPCs;
using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Accessories;
using WarframeMod.Content.Items.Weapons;

namespace WarframeMod.Content.Buffs;

public class ScoliacDebuff : ModBuff
{
    public override string Texture => "Terraria/Images/Buff_" + BuffID.ThornWhipNPCDebuff;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Scoliac");
        Main.buffNoSave[Type] = true;
        Main.debuff[Type] = true;
    }
}
internal class ScoliacDebuffGlobalNPC : GlobalNPC
{
    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (npc.HasBuff<ScoliacDebuff>() && projectile.DamageType == DamageClass.Summon)
        {
            damage += Scoliac.TAG_DAMAGE;
            if (Main.rand.Next(0, 100) < Scoliac.TAG_POISON_CHANCE)
                npc.AddBuff(BuffID.Poisoned, 600);
            if (Main.rand.Next(0, 100) < Scoliac.TAG_VENOM_CHANCE)
                npc.AddBuff(BuffID.Venom, 420);
        }
    }
}