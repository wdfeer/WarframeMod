using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Content.Buffs;
public class ArcaneArachneBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Arachne Cooldown");
        Description.SetDefault($"-{ArcaneArachne.DAMAGE_BUFF}% Damage");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Main.debuff[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Generic) -= ArcaneArachne.DAMAGE_BUFF / 100f;
    }
}
