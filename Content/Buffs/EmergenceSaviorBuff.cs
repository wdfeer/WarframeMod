namespace WarframeMod.Content.Buffs;
public class EmergenceSaviorBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Emergence Savior Cooldown");
        Description.SetDefault($"Cannot negate lethal damage\nIf Emergence Savior is not equipped, damage taken is increased tenfold");
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Main.debuff[Type] = true;
    }
}
