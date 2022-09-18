using WarframeMod.Content.Items.Arcanes;

namespace WarframeMod.Content.Buffs;
public class ArcaneFuryBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Fury");
        Description.SetDefault($"+{ArcaneFury.DAMAGE_BUFF}% melee Damage");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Melee) += ArcaneFury.DAMAGE_BUFF / 100f;
    }
}
