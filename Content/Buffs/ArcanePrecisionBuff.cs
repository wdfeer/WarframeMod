using Terraria;
using Terraria.ModLoader;
using WarframeMod.Common.Players;
using WarframeMod.Content.Items.Accessories;

namespace WarframeMod.Content.Buffs;
public class ArcanePrecisionBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Arcane Precision");
        Description.SetDefault($"+{(int)(ArcanePrecision.DAMAGE_BUFF_PREHARDMODE * 100)}% Damage");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }
    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        tip = $"+{(int)(ArcanePrecision.DamageBuff * 100)}% Damage";
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Generic) += ArcanePrecision.DamageBuff;
    }
}
